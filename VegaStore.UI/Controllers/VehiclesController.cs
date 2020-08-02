using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VegaStore.Core.DbQueryFeatures;
using VegaStore.Core.Repositories;
using VegaStore.UI.ViewModels;
using VegaStore.UI.ViewModels.RequestFeatureViewModels;
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

        public async Task<IActionResult> Index(PublicVehicleRequestParametersVM vehicleRequestParametersVM)
        {
            var vehicleQueryParameters = _mapper.Map<VehicleQueryParameters>(vehicleRequestParametersVM);
            
            var queryResult = await _repository.Vehicles
                .GetAllVehiclesAsync(vehicleQueryParameters, trackChanges: false);

            var listOfVehicleVM = _mapper.Map<IEnumerable<VehicleViewModel>>(queryResult.Items);

            vehicleRequestParametersVM.TotalItemsCount = queryResult.ItemCount;

            var listVehicleVM = new PublicListVehicleViewModel
            {
                Vehicles = listOfVehicleVM,
                //PaginationDetails = new PaginationDetails
                //{
                //    HasNext = vehicleRequestParametersVM.HasNext,
                //    HasPrevious = vehicleRequestParametersVM.HasPrevious,
                //    Length = vehicleRequestParametersVM.Length,
                //    Start = vehicleRequestParametersVM.Start,
                //    TotalItemsCount = vehicleRequestParametersVM.TotalItemsCount
                //}
                PaginationDetails = _mapper.Map<PaginationDetails>(vehicleRequestParametersVM)
            };

            return View(listVehicleVM);
        }
    }
}