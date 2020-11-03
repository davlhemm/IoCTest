
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;

namespace IoCTest
{
    public static class ControllerHelper
    {
        /// <summary>
        /// Dump all property values from json config into ViewData per-property name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        public static void ViewDataPopulator<T,TT,TDict>(T options, TT baseClass, TDict controller)
            where TT : IBase
            where TDict : IDictionary<string, object>
        {
            IEnumerable<PropertyInfo> propInfos = options.GetType().GetRuntimeProperties();

            foreach (var theProp in propInfos)
            {
                controller[theProp.Name] = baseClass.BaseDo(theProp.GetValue(options).ToString());
            }
        }

        public static T OptionBinder<T>(string optionKey, IConfiguration config)
            where T: new()
        {
            var options = new T();

            config.GetSection(optionKey).Bind(options);

            return options;
        }
    }
}