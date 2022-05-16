using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zikunov.ServiceStation.WebApp.Areas.Identity.Data;

namespace Zikunov.ServiceStation.WebApp.Data
{
    public class ZikunovServiceStationWebAppContext : IdentityDbContext<ZikunovServiceStationWebAppUser>
    {
        public ZikunovServiceStationWebAppContext(DbContextOptions<ZikunovServiceStationWebAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
