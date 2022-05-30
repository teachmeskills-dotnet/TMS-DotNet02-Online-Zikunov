using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Zikunov.ServiceStation.Data.Contexts;
using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.Logic.Interfaces;
using Zikunov.ServiceStation.Logic.Managers;
using Zikunov.ServiceStation.Logic.Services;
using Zikunov.ServiceStation.WebApi.Middlewares;
using Zikunov.ServiceStation.WebApi.Settings;

namespace Zikunov.ServiceStation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryManager<>), typeof(RepositoryManager<>));
            services.AddScoped<IVehicleManager, VehicleManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddControllers();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "ServiceStation API",
                    Contact = new OpenApiContact() { }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                config.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[] { "Bearer" }
                    }
                };
                config.AddSecurityRequirement(securityRequirement);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zikunov.ServiceStation.WebApi v1"));
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
