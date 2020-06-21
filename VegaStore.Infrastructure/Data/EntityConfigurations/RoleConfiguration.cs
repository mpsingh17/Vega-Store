using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Infrastructure.Data.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Id = "f2a5d543-1e36-40d8-b90b-10c0488eb920",
                ConcurrencyStamp = "afb3404f-119b-41b6-be43-d7a8f27ad955",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "b879cf0d-10f0-485a-ac74-5bfb46d74d73",
                ConcurrencyStamp = "e167fed0-fd03-4570-b63a-a1320ff76baa",
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            });
        }
    }
}
