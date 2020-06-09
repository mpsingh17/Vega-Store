using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VegaCore.Infrastructure.Data.EntityConfigurations;
using VegaStore.Core.Entities;

namespace VegaCore.Infrastructure.Data
{
    public class EFCoreContext : DbContext
    {
        public EFCoreContext(DbContextOptions<EFCoreContext> options)
            : base(options) {}

        public DbSet<Make> Makes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MakeConfiguration());
        }
    }
}
