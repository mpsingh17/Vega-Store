using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VegaStore.Core.Constants;
using VegaStore.Core.Repositories;
using VegaStore.UI.Areas.Admin.ViewModels.FileOnFileSystemViewModels;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<MediaController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MediaController(
            IWebHostEnvironment hostEnvironment,
            ILogger<MediaController> logger,
            IRepositoryManager repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var filesOnFileSystem = await _repository.FilesOnFileSystem.GetAllFilesOnFileSystemAsync(trackChanges: false);

            var result = _mapper.Map<IEnumerable<ListFileOnFileSystemViewModel>>(filesOnFileSystem);

            return View(result);
        }

        [HttpDelete]
        [SkipStatusCodePages]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var fileObjInDb = await _repository.FilesOnFileSystem.GetSingleFileOnFileSystemAsync(id, trackChanges: false);

            if (fileObjInDb is null)
            {
                _logger.LogWarning(LogEventId.Warning, "Invalid file object requested. ID = {id}", id);
                return NotFound();
            }
            var filePathToDelete = Path.Combine(_hostEnvironment.WebRootPath, "uploads", fileObjInDb.Path);
            _logger.LogInformation($"File to delete is: {filePathToDelete}");

            if (System.IO.File.Exists(filePathToDelete))
            {
                System.IO.File.Delete(filePathToDelete);

                _repository.FilesOnFileSystem.Remove(fileObjInDb);
                await _repository.SaveAsync();
                _logger.LogInformation(LogEventId.Success, "File {fileName} has been deleted.", Path.GetFileName(filePathToDelete));
            }
            else
            {
                _logger.LogWarning(LogEventId.Warning, "File {fileName} is requested to delete but not found.", Path.GetFileName(filePathToDelete));
            }

            return Ok($"Media file has been deleted.");
        }
    }
}