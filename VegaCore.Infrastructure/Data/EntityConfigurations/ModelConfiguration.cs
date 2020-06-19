using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(m => m.Make)
                .WithMany(ma => ma.Models)
                .HasForeignKey(m => m.MakeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
