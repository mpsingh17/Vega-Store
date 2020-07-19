using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.DbQueryFeatures;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.RequestFeatures;
using VegaStore.Infrastructure.Data.Extensions;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(EFCoreContext context)
            : base(context) {}

        public async Task<QueryResult<Vehicle>> GetAllVehiclesAsync(VehicleParameters vehicleParameters, bool trackChanges)
        {
            var query = GetAll(trackChanges);

            query = query.Include(v => v.Model).AsQueryable();

            query = query.ApplyFilters(vehicleParameters);

            var vehiclesCount = await query.CountAsync();

            query = query.ApplyPagination<Vehicle>(vehicleParameters.Start, vehicleParameters.Length);

            var vehicles = await query.ToListAsync();

            return new QueryResult<Vehicle> { Items = vehicles, ItemCount = vehiclesCount };
        }

        public async Task<int> GetVehiclesCount() => await GetAll(trackChanges: false).CountAsync();

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
