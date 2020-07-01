using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.HasKey(vi => vi.Id);

            builder.Property(vi => vi.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(vi => vi.IsFeatured)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
