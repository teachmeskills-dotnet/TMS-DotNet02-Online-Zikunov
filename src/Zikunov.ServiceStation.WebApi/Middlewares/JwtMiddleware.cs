﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.WebApi.Models;
using Zikunov.ServiceStation.WebApi.Settings;

namespace Zikunov.ServiceStation.WebApi.Middlewares
{
    /// <summary>
    /// Jwt middleware.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="next">Next request delegate.</param>
        /// <param name="appSettings">App settings.</param>
        /// <param name="serviceScopeFactory">Service scope factory.</param>
        public JwtMiddleware(
            RequestDelegate next, 
            IOptions<AppSettings> appSettings,
            IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));

            if(appSettings is null)
            {
                 throw new ArgumentNullException(nameof(serviceScopeFactory));
            }
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
            {
                await AttachUserToContextAsync(context, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContextAsync(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //set clockskew to zero so tokens expire exactly at token axpiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                //attach user to context on successful jwt validation
                using var scope = _serviceScopeFactory.CreateScope();
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

                var user = await userManager.FindByIdAsync(userId);
                var userModel = new UserModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    IsActive = user.IsActive
                };

                context.Items["User"] = userModel;
            }
            catch
            {
                //do nothing if jwt validation fails
                //user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
