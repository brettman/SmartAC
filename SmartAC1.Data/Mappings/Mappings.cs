using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartAC1.Core.Models;

namespace SmartAC1.Data.Mappings
{
    public static class Mappings
    {
        public static ModelBuilder MapDevice(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Device>();

            entity.HasKey(i => i.SerialNr);
            //entity
            //    .HasIndex(i => i.SerialNr)
            //    .IsUnique();
            entity
                .Property(i => i.SerialNr)
                .HasMaxLength(30);
            entity
                .HasMany(i => i.SensorData);

            return modelBuilder;
        }

        public static ModelBuilder MapSensorData(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SensorData>();

            entity.HasIndex(i => i.SubmissionTime);

            entity
                .Property(i => i.SerialNr)
                .HasMaxLength(30);
            entity
                .Property(i => i.DeviceHealthStatus)
                .HasMaxLength(150);

            entity
                .Property(i => i.TemperatureInCelsius)
                .HasColumnType("float");
            entity
                .Property(i => i.HumidityPercentage)
                .HasColumnType("float");
            entity
                .Property(i => i.CarbonMonoxidePpm)
                .HasColumnType("float");

            return modelBuilder;
        }
    }
}
