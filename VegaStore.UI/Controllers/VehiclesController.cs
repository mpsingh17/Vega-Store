using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.RequestFeatures;
using VegaStore.UI.ViewModels.Public.VehicleViewModels;

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

        public async Task<IActionResult> Index(VehicleParameters vehicleParameters)
        {
            var queryResult = await _repository.Vehicles.GetAllVehiclesAsync(vehicleParameters, trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListVehicleViewModel>>(queryResult.Items);

            var paginatedListVehicleVM = new PaginatedListVehicleViewModel
            {
                TotalVehicles = queryResult.ItemCount,
                Vehicles = result,
                Start = vehicleParameters.Start,
                Length = vehicleParameters.Length
            };

            return View(paginatedListVehicleVM);
        }
    }
}