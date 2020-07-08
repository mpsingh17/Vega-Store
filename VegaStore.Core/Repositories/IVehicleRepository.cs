using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VegaStore.Core.Entities;
using VegaStore.Core.RequestFeatures;

namespace VegaStore.Core.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        //Task<IEnumerable<Vehicle>> GetAllVehiclesAsync(VehicleParameters vehicleParameters, bool trackChanges);
        PagedList<Vehicle> GetAllVehiclesAsync(VehicleParameters vehicleParameters, bool trackChanges);
        Task<int> GetVehiclesCount();
        Task<Vehicle> GetSingleVehicleAsync(int id, bool includeRelated, bool trackChanges);
        Task<Vehicle> GetSingleVehicleAsync(int id, bool trackChanges);
    }
}
