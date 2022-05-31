using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zikunov.ServiceStation.Data.Constants;
using Zikunov.ServiceStation.Data.Enums;
using Zikunov.ServiceStation.Data.Models;

namespace Zikunov.ServiceStation.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Service entity.
    /// </summary>
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Services, Schema.Tracker)
                .HasKey(service => service.ServiceId);

            builder.Property(service => service.ServiceId)
                .UseIdentityColumn();

            builder.Property(service => service.Comment)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(service => service.Start)
                .HasColumnType(SqlConfiguration.SqlSmallDateFormat);

            builder.Property(service => service.End)
                .HasColumnType(SqlConfiguration.SqlSmallDateFormat);

            builder.Property(service => service.TypeOfService)
               .HasConversion(new EnumToNumberConverter<ServiceType, int>());

            builder.HasOne(service => service.Vehicle)
                .WithMany(vehicle => vehicle.Services)
                .HasForeignKey(service => service.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
