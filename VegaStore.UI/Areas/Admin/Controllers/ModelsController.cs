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
using VegaStore.UI.Areas.Admin.ViewModels.ModelViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelsController : Controller
    {
        private readonly ILogger<ModelsController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ModelsController(
            ILogger<ModelsController> logger,
            IRepositoryManager repository,
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var modelsInDb = _repository.Models.Models;

            var result = _mapper.Map<IEnumerable<ListModelViewModel>>(modelsInDb);

            return View(result);
        }

        [ImportModelState]
        public IActionResult Create()
        {
            var userId = _userService.GetUserId(User);

            var vm = new CreateModelViewModel
            {
                MakeSLIs = _repository.Makes
                .GetAllMakesAsync(trackChanges: false)
                .Result
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).ToList()

            };

            return View(vm);
        }

        [HttpPost]
        [ExportModelState]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckMakeOfModelExists))]
        public async Task<IActionResult> Create(CreateModelViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            var userId = _userService.GetUserId(User);

            var modelToCreate = _mapper.Map<Model>(vm);
            modelToCreate.UserId = userId;
            modelToCreate.CreatedAt = DateTime.Now;
            modelToCreate.UpdatedAt = DateTime.Now;

            await _repository.Models.AddAsync(modelToCreate);

            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
        
        [ImportModelState]
        [ServiceFilter(typeof(CheckModelExists))]
        public IActionResult Edit(int id)
        {            
            Model modelInDb = HttpContext.Items[nameof(modelInDb)] as Model;

            var vm = new EditModelViewModel
            {
                Name = modelInDb.Name,
                MakeId = modelInDb.MakeId,
                MakeSLIs = _repository.Makes
                .GetAllMakesAsync(trackChanges: false)
                .Result
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).ToList()
            };

            return View(vm);
        }
    
        [HttpPost]
        [ExportModelState]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckModelExists))]
        [ServiceFilter(typeof(CheckMakeOfModelExists))]
        public async Task<IActionResult> Edit(EditModelViewModel vm, int id)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { id });
            }

            Model modelInDb = HttpContext.Items[nameof(modelInDb)] as Model;

            _mapper.Map(vm, modelInDb);
            modelInDb.UpdatedAt = DateTime.Now;

            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }   
    
        [HttpDelete]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CheckModelExists))]
        public async Task<IActionResult> Delete(int id)
        {
            Model modelInDb = HttpContext.Items[nameof(modelInDb)] as Model;

            _repository.Models.Remove(modelInDb);

            await _repository.SaveAsync();

            return Ok("Model deleted successfully");
        }
    }
}