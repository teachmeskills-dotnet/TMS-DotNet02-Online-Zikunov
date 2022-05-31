using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using Zikunov.ServiceStation.Core.Enums;
using Zikunov.ServiceStation.Data.Constants;
using Zikunov.ServiceStation.Data.Models;

namespace Zikunov.ServiceStation.Data.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        /// <summary>
        /// EF Configuration for Vehicle entity.
        /// </summary>
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Vehicles, Schema.Tracker)
                .HasKey(vehicle => vehicle.Id);

            builder.Property(vehicle => vehicle.Id)
                .UseIdentityColumn();

            builder.Property(vehicle => vehicle.Brand)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(vehicle => vehicle.Number)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(vehicle => vehicle.VehicleType)
               .HasConversion(new EnumToNumberConverter<VehicleType, int>());

            builder.HasOne(vehicle => vehicle.User)
                .WithMany(user => user.Vehicles)
                .HasForeignKey(vehicle => vehicle.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
