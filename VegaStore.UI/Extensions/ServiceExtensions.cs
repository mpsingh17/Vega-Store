using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Repositories;
using VegaStore.Core.Services;
using VegaStore.Infrastructure.Data.Repositories;
using VegaStore.Infrastructure.Services;

namespace VegaStore.UI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureEFCoreContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFCoreContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("VegaStore.UI"))
            );
        }
    }
}
