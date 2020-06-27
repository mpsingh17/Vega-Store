using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaStore.Core.Entities;

namespace VegaStore.Core.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(bool trackChanges);
        
        Task<Vehicle> GetSingleVehicleAsync(int id, bool includeRelated, bool trackChanges);
        Task<Vehicle> GetSingleVehicleAsync(int id, bool trackChanges);
    }
}
