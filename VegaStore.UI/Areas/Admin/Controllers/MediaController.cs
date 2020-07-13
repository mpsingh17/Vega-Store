using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VegaStore.Core.Repositories;
using VegaStore.UI.ViewModels.FileOnFileSystemViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly ILogger<MediaController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MediaController(
            ILogger<MediaController> logger,
            IRepositoryManager repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var filesOnFileSystem = await _repository.FilesOnFileSystem.GetAllFilesOnFileSystemAsync(trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListFileOnFileSystemViewModel>>(filesOnFileSystem);

            return View(result);
        }
    }
}