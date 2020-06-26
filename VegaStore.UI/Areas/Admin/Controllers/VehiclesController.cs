using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.Services;
using VegaStore.UI.ActionFilters;
using VegaStore.UI.ViewModels.VehicleViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehiclesController : Controller
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public VehiclesController(
            ILogger<VehiclesController> logger,
            IRepositoryManager repository,
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var vehiclesInDb = await _repository.Vehicles.GetAllVehiclesAsync(trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListVehicleViewModel>>(vehiclesInDb);

            return View(result);
        }

        [ImportModelState]
        public IActionResult Create()
        {
            var vm = new CreateVehicleViewModel
            {
                ModelSLIs = _repository.Models.Models.Select(
                    m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }
                )
            };

            return View(vm);
        }

        [HttpPost]
        [ExportModelState]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVehicleViewModel vm)
        {
            if(!ModelState.IsValid)
                return RedirectToAction(nameof(Create));

            var modelInDb = await _repository.Models.GetSingleModelAsync(vm.ModelId, trackChanges: false);
            if(modelInDb is null)
            {
                ModelState.AddModelError(nameof(vm.ModelId), "Invalid model selected.");
                return RedirectToAction(nameof(Create));
            }

            var vehicleToCreate = new Vehicle
            {
                Name = vm.Name,
                Price = Convert.ToDecimal(vm.Price),
                ModelId = vm.ModelId,
                Color = vm.Color,
                Condition = vm.Condition,
                IsRegistered = vm.IsRegistered,
                UserId = _userService.GetUserId(User),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _repository.Vehicles.AddAsync(vehicleToCreate);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}