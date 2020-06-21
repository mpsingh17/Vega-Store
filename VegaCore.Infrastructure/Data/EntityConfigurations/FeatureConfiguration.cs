using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
