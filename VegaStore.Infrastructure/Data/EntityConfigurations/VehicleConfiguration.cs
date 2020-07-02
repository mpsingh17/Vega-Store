using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            #region Column configuration

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(v => v.IsRegistered)
                .IsRequired();

            var colorConverter = new EnumToNumberConverter<Colors, byte>();
            builder.Property(v => v.Color)
                .IsRequired()
                .HasConversion(colorConverter);

            var conditionConverter = new EnumToNumberConverter<Condition, byte>();
            builder.Property(v => v.Condition)
                .IsRequired()
                .HasConversion(conditionConverter);

            builder.Property(v => v.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(v => v.FeatureImage)
                .IsRequired()
                .HasMaxLength(255);
            #endregion

            #region Relationship configuration

            builder.HasOne(v => v.Model)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(v => v.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.VehicleFeatures)
                .WithOne(vf => vf.Vehicle)
                .HasForeignKey(vf => vf.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.VehicleImages)
                .WithOne(vi => vi.Vehicle)
                .HasForeignKey(vi => vi.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        
        }
    }
}
