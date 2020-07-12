using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VegaStore.Core.Entities;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class FileOnFileSystemConfiguration : IEntityTypeConfiguration<FileOnFileSystem>
    {
        public void Configure(EntityTypeBuilder<FileOnFileSystem> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Path)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.Extension)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(f => f.CreatedAt)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
