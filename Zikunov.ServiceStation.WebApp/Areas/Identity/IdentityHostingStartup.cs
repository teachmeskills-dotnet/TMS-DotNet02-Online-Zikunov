using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zikunov.ServiceStation.WebApp.Areas.Identity.Data;
using Zikunov.ServiceStation.WebApp.Data;

[assembly: HostingStartup(typeof(Zikunov.ServiceStation.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace Zikunov.ServiceStation.WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ZikunovServiceStationWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ZikunovServiceStationWebAppContextConnection")));

                services.AddDefaultIdentity<ZikunovServiceStationWebAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<ZikunovServiceStationWebAppContext>();
            });
        }
    }
}