using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(EFCoreContext context)
            : base(context) {}

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .ToListAsync();
        }

        public async Task<Vehicle> GetSingleVehicleAsync(int id, bool trackChanges)
        {
            return await FindByCondition(v => v.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
