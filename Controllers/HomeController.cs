using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSIT133Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT133Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult FirstAjax()
        {
            return View();
        }
        public IActionResult AjaxPost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AjaxPost(User user)
        {
            ViewBag.name = user.name;
            return View();
        }
        public IActionResult AjaxFormData(User user)
        {
            ViewBag.name = user.name;
            return View();
        }
        public IActionResult Promise()
        {
            return View();
        }
    }
}
