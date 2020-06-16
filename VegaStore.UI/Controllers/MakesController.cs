using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VegaCore.Infrastructure.Data;
using VegaStore.Core.Entities;
using VegaStore.Core.Repositories;
using VegaStore.Core.ViewModels.MakeViewModels;

namespace VegaStore.UI.Controllers
{
    public class MakesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MakesController(
            UserManager<IdentityUser> userManager,
            IRepositoryManager repository,
            IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Makes
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var makes = await _repository.Makes.GetAllMakesAsync(userId, trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListMakeViewModel>>(makes);

            return View(result);
        }

        // GET: Makes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveMakeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var make = _mapper.Map<Make>(vm);
                make.UserId = _userManager.GetUserId(User);
                make.CreatedAt = DateTime.Now;
                make.UpdatedAt = DateTime.Now;

                await _repository.Makes.AddAsync(make);
                await _repository.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Makes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var make = await _repository.Makes.GetSingleMakeAsync(userId, (int)id, trackChanges: false);

            if (make == null)
                return NotFound();

            var result = _mapper.Map<SaveMakeViewModel>(make);

            return View(result);
        }

        // POST: Makes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaveMakeViewModel vm)
        {
            var userId = _userManager.GetUserId(User);
            
            var makeInDb = await _repository.Makes.GetSingleMakeAsync(userId, id, trackChanges: true);

            if (makeInDb == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(vm);

            _mapper.Map(vm, makeInDb);
            makeInDb.UpdatedAt = DateTime.Now;

            await _repository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
