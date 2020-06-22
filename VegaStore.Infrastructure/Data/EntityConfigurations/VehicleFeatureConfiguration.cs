using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class VehicleFeatureConfiguration : IEntityTypeConfiguration<VehicleFeature>
    {
        public void Configure(EntityTypeBuilder<VehicleFeature> builder)
        {
            builder.ToTable("VehicleFeatures");

            builder.HasKey(vf => new { vf.FeatureId, vf.VehicleId });
        }
    }
}
