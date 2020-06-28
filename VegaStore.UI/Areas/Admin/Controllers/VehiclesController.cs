using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
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
        public async Task<IActionResult> Create()
        {
            var featuresInDb = await _repository.Features.GetAllFeaturesAsync(trackChanges: false);
            var vm = new CreateVehicleViewModel
            {
                ModelSLIs = _repository.Models.Models.Select(
                    m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }
                ),
                FeatureSLIs = featuresInDb
                    .Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() })
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

            var featuresToAdd = await _repository.Features.GetAllFeaturesAsync(vm.FeatureIds, trackChanges: false);
            if(featuresToAdd is null)
            {
                ModelState.AddModelError(nameof(vm.FeatureIds), "Invalid feature(s) selected.");
                return RedirectToAction(nameof(Create));
            }

            var vehicleToCreate = new Vehicle
            {
                Name = vm.Name,
                Price = Convert.ToDecimal(vm.Price),
                ModelId = vm.ModelId,
                VehicleFeatures = featuresToAdd.Select(f => new VehicleFeature { FeatureId = f.Id}).ToList(),
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

        public async Task<IActionResult> Edit(int id)
        {
            var vehicleInDb = await _repository.Vehicles.GetSingleVehicleAsync(id, includeRelated: true, trackChanges: false);
            if (vehicleInDb is null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<EditVehicleViewModel>(vehicleInDb);

            vm.ModelSLIs = _repository.Models.Models
                .Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            var featuresInDb = await _repository.Features.GetAllFeaturesAsync(trackChanges: false);
            vm.FeatureSLIs = featuresInDb.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() });

            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var vehicleInDb = await _repository.Vehicles.GetSingleVehicleAsync(id, includeRelated: true, trackChanges: false);
            if(vehicleInDb is null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<DetailVehicleViewModel>(vehicleInDb);

            return View(vm);
        }


    }
}