using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.RequestFeatures;

namespace VegaStore.Infrastructure.Data.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(EFCoreContext context)
            : base(context) {}

        //public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(VehicleParameters vehicleParameters, bool trackChanges)
        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(VehicleParameters vehicleParameters, bool trackChanges)
        {
            var query = GetAll(trackChanges);

            var searchTerm = vehicleParameters.Search?.Value;
            if (searchTerm != null)
                query = query.Where(v => v.Name.Contains(searchTerm.Trim().ToLower()));

            if (Enum.TryParse(vehicleParameters.Color, out Colors color))
            {
                var totalEnumItems = Enum.GetNames(typeof(Colors)).Length;

                if (color > 0 && (int)color <= totalEnumItems)
                    query = query.Where(v => v.Color.Equals(color));
            }
            
            if (Enum.TryParse(vehicleParameters.Condition, out Condition condition))
            {
                var totalEnumItems = Enum.GetNames(typeof(Condition)).Length;

                if (condition > 0 && (int)condition <= totalEnumItems)
                    query = query.Where(v => v.Condition.Equals(condition));
            }

            if (vehicleParameters.MinPrice != null && vehicleParameters.MaxPrice != null)
            {
                if (vehicleParameters.MinPrice > 0 && vehicleParameters.MinPrice < vehicleParameters.MaxPrice)
                {
                    query = query.Where(v => v.Price >= vehicleParameters.MinPrice && v.Price <= vehicleParameters.MaxPrice);
                }
            }

            query = query.Include(v => v.Model);

            return await PagedList<Vehicle>.ToPagedList(query, vehicleParameters.Start, vehicleParameters.Length);
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
