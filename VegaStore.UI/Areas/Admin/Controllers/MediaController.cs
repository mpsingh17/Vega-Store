using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VegaStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}