using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Zikunov.ServiceStation.Data.Configurations;
using Zikunov.ServiceStation.Data.Models;

namespace Zikunov.ServiceStation.Data.Contexts
{
    /// <summary>
    /// Main application context.
    /// </summary>
    public class ApplicationContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for Vehicles.
        /// </summary>
        public DbSet<Service> Vehicles { get; set; }

        /// <summary>
        /// DbSet for Services.
        /// </summary>
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
