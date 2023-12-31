﻿using Microsoft.AspNetCore.Mvc;
using prjAjaxDemo.Models;
using System.Diagnostics;

namespace prjAjaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;
		public HomeController(ILogger<HomeController> logger, DemoContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Register()
        {
            
            return View(_context.Members);
        }
        public IActionResult Member()
        {
            return View(_context.Members);
        }
        public IActionResult jQuery()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult Partial1()
        {
            return PartialView();
        }
        public IActionResult Partial2()
        {
            ViewBag.gay = "URGayFromPartial2";
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult First()
        {
            return View();
        }
        public IActionResult Address() 
        {
            return View();
        }
        public IActionResult Fetch()
        {
            return View();
        }
    }
}