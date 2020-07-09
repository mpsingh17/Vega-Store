using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VegaStore.UI.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
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
                        break;
                    default:
                        break;
                }
            }

            return View(viewName);
        }
    }
}