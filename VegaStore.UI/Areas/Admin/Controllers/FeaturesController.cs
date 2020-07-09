using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.Services;
using VegaStore.UI.ActionFilters;
using VegaStore.UI.ViewModels.FeatureViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeaturesController : Controller
    {
        private readonly ILogger<FeaturesController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FeaturesController(
            ILogger<FeaturesController> logger,
            IRepositoryManager repository,
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        
        public async Task<IActionResult> Index()
        {
            var featuresInDb = await _repository.Features.GetAllFeaturesAsync(trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListFeatureViewModel>>(featuresInDb);

            return View(result);
        }

        [ImportModelState]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ExportModelState]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFeatureViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            var userId = _userService.GetUserId(User);

            var featureToCreate = _mapper.Map<Feature>(vm);
            featureToCreate.UserId = userId;
            featureToCreate.CreatedAt = DateTime.Now;
            featureToCreate.UpdatedAt = DateTime.Now;

            await _repository.Features.AddAsync(featureToCreate);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [ImportModelState]
        [ServiceFilter(typeof(CheckFeatureExists))]
        public IActionResult Edit(int id)
        {
            Feature featureInDb = HttpContext.Items[nameof(featureInDb)] as Feature;

            var vm = _mapper.Map<EditFeatureViewModel>(featureInDb);

            return View(vm);
        }

        [HttpPost]
        [ExportModelState]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckFeatureExists))]
        public async Task<IActionResult> Edit(EditFeatureViewModel vm, int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { id });
            }

            Feature featureInDb = HttpContext.Items[nameof(featureInDb)] as Feature;

            _mapper.Map(vm, featureInDb);
            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckFeatureExists))]
        public async Task<IActionResult> Delete(int id)
        {
            Feature featureInDb = HttpContext.Items[nameof(featureInDb)] as Feature;

            _repository.Features.Remove(featureInDb);
            await _repository.SaveAsync();

            return Ok($"{featureInDb.Name} Feature has been deleted.");
        }

    }
}