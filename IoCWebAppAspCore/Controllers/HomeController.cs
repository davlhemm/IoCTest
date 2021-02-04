using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IoCWebAppAspCore.Models;
using IoCTest;
using IoCTest.Model;
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
        private readonly IoCTest.Model.ILogger _myLogger;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IBase baseThing, IoCTest.Model.ILogger myLogger)
        {
            _logger   = logger;
            _config   = config;
            _base     = baseThing;
            _myLogger = myLogger;
        }

        public IActionResult Index()
        {
            string aTest = _base.BaseDo("In Index of HomeController");
            _logger.LogInformation(aTest);

            var _iocOptions = _config.OptionBinder<BaseOptions>(BaseOptions.Base);
            _iocOptions.ViewDataPopulator(_base, ViewData);
            
            return View();
        }

        public IActionResult Privacy()
        {
            var _iocOptions = _config.OptionBinder<BaseOptions>(BaseOptions.Base);
            _iocOptions.ViewDataPopulator(_base, ViewData);

            return View();
        }

        public IActionResult Test()
        {
            var _iocOptions = _config.OptionBinder<OtherOptions>(OtherOptions.Other);
            _iocOptions.ViewDataPopulator(_base, ViewData);

            _myLogger.Log(_iocOptions.Name);

            return View(new TestViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Other()
        {
            var _iocOptions = _config.OptionBinder<TestingOptions>(TestingOptions.Testing);
            _iocOptions.ViewDataPopulator(_base, ViewData);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
