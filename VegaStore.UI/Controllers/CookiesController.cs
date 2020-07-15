using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;

namespace VegaStore.UI.Controllers
{
    public class CookiesController : Controller
    {
        public IActionResult Index()
        {
            var cookies = Request.Cookies;

            return View(cookies);
        }

        public IActionResult Create()
        {
            var key = "navbar_color";
            var value = "blue";
            int? expireTime = 2;

            var cookieOptions = new CookieOptions();

            if (expireTime.HasValue)
                cookieOptions.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                cookieOptions.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, cookieOptions);

            return View();
        }
    }
}