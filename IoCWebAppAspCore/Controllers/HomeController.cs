using System;
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

            var _iocOptions = new BaseOptions();
            _config.GetSection(BaseOptions.Base).Bind(_iocOptions);

            ViewData["Title"] = _iocOptions.Title;

            return View();
        }

        public IActionResult Privacy()
        {
            var _iocOptions = new BaseOptions();
            _config.GetSection(BaseOptions.Base).Bind(_iocOptions);

            ViewDataPopulator(_iocOptions);

            return View();
        }

        public IActionResult Test()
        {
            var _iocOptions = new OtherOptions();
            _config.GetSection(OtherOptions.Other).Bind(_iocOptions);

            ViewDataPopulator(_iocOptions);

            return View();
        }

        public IActionResult Other()
        {
            return View();
        }

        /// <summary>
        /// Dump all property values from json config into ViewData per-property name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        public void ViewDataPopulator<T>(T options)
        {
            IEnumerable<PropertyInfo> propInfos = options.GetType().GetRuntimeProperties();

            foreach (var theProp in propInfos)
            {
                ViewData[theProp.Name] = _base.BaseDo(theProp.GetValue(options).ToString());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
