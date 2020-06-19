using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VegaCore.Infrastructure.Data.EntityConfigurations;
using VegaStore.Core.Entities;
using VegaStore.Infrastructure.Data.EntityConfigurations;

namespace VegaCore.Infrastructure.Data
{
    public class EFCoreContext : DbContext
    {
        public EFCoreContext(DbContextOptions<EFCoreContext> options)
            : base(options) {}

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MakeConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
        }
    }
}
