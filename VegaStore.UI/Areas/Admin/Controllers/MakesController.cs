using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Constants;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.Services;
using VegaStore.Core.ViewModels.MakeViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MakesController : Controller
    {
        private readonly ILogger<MakesController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public MakesController(
            ILogger<MakesController> logger,
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
            var userId = _userService.GetUserId(User);

            var makes = await _repository.Makes.GetAllMakesAsync(userId, trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListMakeViewModel>>(makes);

            _logger.LogInformation(LogEventId.Success, "{MakesCount} Makes have been sent.", result.Count());
            return View(result);
        }

        public IActionResult Create()
        {
            _logger.LogInformation(LogEventId.Success, "View to create Make requested.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveMakeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make view model sent by client.");
                return View(vm);
            }

            var make = _mapper.Map<Make>(vm);
            make.UserId = _userService.GetUserId(User);
            make.CreatedAt = DateTime.Now;
            make.UpdatedAt = DateTime.Now;

            await _repository.Makes.AddAsync(make);
            await _repository.SaveAsync();

            _logger.LogInformation(LogEventId.Success, "Make with ID = {MakeId} has been created.", make.Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", id);
                return NotFound();
            }

            var userId = _userService.GetUserId(User);

            var make = await _repository.Makes.GetSingleMakeAsync(userId, (int)id, trackChanges: false);

            if (make == null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", id);
                return NotFound();
            }

            var result = _mapper.Map<SaveMakeViewModel>(make);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaveMakeViewModel vm)
        {
            var userId = _userService.GetUserId(User);
            
            var makeInDb = await _repository.Makes.GetSingleMakeAsync(userId, id, trackChanges: true);

            if (makeInDb == null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", id);
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make view model sent by client.");
                return View(vm);
            }

            _mapper.Map(vm, makeInDb);
            makeInDb.UpdatedAt = DateTime.Now;

            await _repository.SaveAsync();

            _logger.LogInformation(LogEventId.Success, "Make with ID = {MakeId} has been updated.", makeInDb.Id); ;
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userId = _userService.GetUserId(User);

            var makeInDb = await _repository.Makes.GetSingleMakeAsync(userId, id, trackChanges: false);

            if (makeInDb == null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid Make ID = {MakeId} sent by client.", id);
                return NotFound();
            }

            _repository.Makes.Remove(makeInDb);
            //await _repository.SaveAsync();

            _logger.LogWarning(LogEventId.Success, "Make with ID = {MakeId} has been deleted.", makeInDb.Id);
            return NoContent();
        }
    }
}
