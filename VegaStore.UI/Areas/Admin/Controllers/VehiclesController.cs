﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<VehiclesController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public VehiclesController(
            IWebHostEnvironment webHostEnvironment,
            ILogger<VehiclesController> logger,
            IRepositoryManager repository,
            IUserService userService,
            IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
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

            var today = DateTime.Now;

            var uploadsFolderLocation = today.Year.ToString() + "/" + today.Month.ToString() + "/" + today.Day.ToString();
            uploadsFolderLocation = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", uploadsFolderLocation);

            if (!Directory.Exists(uploadsFolderLocation))
                Directory.CreateDirectory(uploadsFolderLocation);

            var uniqueImageName = Guid.NewGuid().ToString() + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.FeaturedImage.FileName);

            var imagePath = Path.Combine(uploadsFolderLocation, uniqueImageName);

            using (FileStream fs = new FileStream(imagePath, FileMode.Create))
            {
                await vm.FeaturedImage.CopyToAsync(fs);
            }

            vehicleToCreate.FeatureImage = uniqueImageName;

            await _repository.Vehicles.AddAsync(vehicleToCreate);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [ImportModelState]
        [ServiceFilter(typeof(CheckVehicleExists))]
        public async Task<IActionResult> Edit(int id)
        {
            Vehicle vehicleInDb = HttpContext.Items[nameof(vehicleInDb)] as Vehicle;

            var vm = _mapper.Map<EditVehicleViewModel>(vehicleInDb);

            vm.ModelSLIs = _repository.Models.Models
                .Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            var featuresInDb = await _repository.Features.GetAllFeaturesAsync(trackChanges: false);
            vm.FeatureSLIs = featuresInDb.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() });

            return View(vm);
        }

        [HttpPost]
        [ExportModelState]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckVehicleExists))]
        public async Task<IActionResult> Edit(EditVehicleViewModel vm, int id)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { id });
            }

            var modelInDb = await _repository.Models.GetSingleModelAsync(vm.ModelId, trackChanges: false);
            if (modelInDb is null)
            {
                ModelState.AddModelError(nameof(vm.ModelId), "Invalid model selected.");
                return RedirectToAction(nameof(Create));
            }

            var featuresToAdd = await _repository.Features.GetAllFeaturesAsync(vm.FeatureIds, trackChanges: false);
            if (featuresToAdd is null)
            {
                ModelState.AddModelError(nameof(vm.FeatureIds), "Invalid feature(s) selected.");
                return RedirectToAction(nameof(Create));
            }

            Vehicle vehicleInDb = HttpContext.Items[nameof(vehicleInDb)] as Vehicle;

            _mapper.Map(vm, vehicleInDb);
            vehicleInDb.UpdatedAt = DateTime.Now;

            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [ServiceFilter(typeof(CheckVehicleExists))]
        public IActionResult Detail(int id)
        {
            Vehicle vehicleInDb = HttpContext.Items[nameof(vehicleInDb)] as Vehicle;

            var vm = _mapper.Map<DetailVehicleViewModel>(vehicleInDb);

            return View(vm);
        }
        
        [HttpDelete]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckVehicleExists))]
        public async Task<IActionResult> Delete(int id)
        {
            Vehicle vehicleInDb = HttpContext.Items[nameof(vehicleInDb)] as Vehicle;

            _repository.Vehicles.Remove(vehicleInDb);
            await _repository.SaveAsync();

            return Ok("Vehicle has been deleted.");
        }

    }
}