using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VegaStore.UI.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Exception")]
        public IActionResult Exception()
        {
            return View();
        }

        [Route("{code}")]
        public IActionResult HandleError(int code)
        {
            string viewName = "default";

            if (code > 0)
            {
                switch (code)
                {
                    case 404:
                        viewName = "NotFound";
                        _logger.LogWarning($"{viewName} view retured.");
                        break;
                    default:
                        break;
                }
            }

            return View(viewName);
        }
    }
}