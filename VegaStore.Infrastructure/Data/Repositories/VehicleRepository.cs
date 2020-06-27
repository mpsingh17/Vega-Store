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
                .Include(v => v.Model)
                .ToListAsync();
        }
        
        public async Task<Vehicle> GetSingleVehicleAsync(int id, bool includeRelated, bool trackChanges)
        {
            var query = FindByCondition(v => v.Id.Equals(id), trackChanges);

            if (includeRelated)
                return await query
                    .Include(v => v.Model)
                    .Include(v => v.VehicleFeatures)
                        .ThenInclude(vf => vf.Feature)
                    .SingleOrDefaultAsync();

            return await query.SingleOrDefaultAsync();
        }

        public async Task<Vehicle> GetSingleVehicleAsync(int id, bool trackChanges)
        {
            return await FindByCondition(v => v.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
