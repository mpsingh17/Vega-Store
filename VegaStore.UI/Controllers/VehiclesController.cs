using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VegaStore.Core.DbQueryFeatures;
using VegaStore.Core.Repositories;
using VegaStore.UI.Areas.Admin.ViewModels.RequestFeaturesViewModels;
using VegaStore.UI.ViewModels.VehicleViewModels;

namespace VegaStore.UI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public VehiclesController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(VehicleParametersViewModel vehicleParametersVM)
        {
            var vehicleQueryParameters = _mapper.Map<VehicleQueryParameters>(vehicleParametersVM);
            
            var queryResult = await _repository.Vehicles.GetAllVehiclesAsync(vehicleQueryParameters, trackChanges: false);

            var vehicleVMs = _mapper.Map<IEnumerable<VehicleViewModel>>(queryResult.Items);

            vehicleParametersVM.TotalItemsCount = queryResult.ItemCount;

            var listVehicleVM = new PublicListVehicleViewModel
            {
                VehicleParametersVM = vehicleParametersVM,
                Vehicles = vehicleVMs
            };

            return View(listVehicleVM);
        }
    }
}