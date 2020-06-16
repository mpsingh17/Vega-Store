using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.UI.Data;

namespace VegaStore.UI.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<EFCoreContext>()
                    .Database
                    .Migrate();

                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>()
                    .Database
                    .Migrate();
            }

            return host;
        }
    }
}
