﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IoCWebAppAspCore.Models;
using IoCTest;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Reflection;

namespace IoCWebAppAspCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IBase _base;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IBase baseThing)
        {
            _logger = logger;
            _config = config;
            _base   = baseThing;
        }

        public IActionResult Index()
        {
            string aTest = _base.BaseDo("In Index of HomeController");
            _logger.LogInformation(aTest);

            var _iocOptions = ControllerHelper.OptionBinder<BaseOptions>(BaseOptions.Base, _config);
            ControllerHelper.ViewDataPopulator(_iocOptions, _base, ViewData);
            
            return View();
        }

        public IActionResult Privacy()
        {
            var _iocOptions = ControllerHelper.OptionBinder<BaseOptions>(BaseOptions.Base, _config);
            ControllerHelper.ViewDataPopulator(_iocOptions, _base, ViewData);

            return View();
        }

        public IActionResult Test()
        {
            var _iocOptions = ControllerHelper.OptionBinder<OtherOptions>(OtherOptions.Other, _config);
            ControllerHelper.ViewDataPopulator(_iocOptions, _base, ViewData);

            return View();
        }

        public IActionResult Other()
        {
            var _iocOptions = ControllerHelper.OptionBinder<TestingOptions>(TestingOptions.Testing, _config);
            ControllerHelper.ViewDataPopulator(_iocOptions, _base, ViewData);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
