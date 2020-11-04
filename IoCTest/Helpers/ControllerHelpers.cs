
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection;

namespace IoCTest
{
    public static class ControllerHelper
    {
        /// <summary>
        /// Dump all property values from json option config into ViewData per-property name.
        /// </summary>
        /// <typeparam name="T">Options type</typeparam>
        /// <typeparam name="TT">Derived from IBase</typeparam>
        /// <typeparam name="TDict"></typeparam>
        /// <param name="options">JSON Options</param>
        /// <param name="baseClass">Fallback class for baseline behavior</param>
        /// <param name="viewData">Controller's ViewData</param>
        public static void ViewDataPopulator<T,TDict>(this T options, IBase baseClass, TDict viewData)
            where TDict : IDictionary<string, object>
        {
            IEnumerable<PropertyInfo> propInfos = options.GetType().GetRuntimeProperties();

            foreach (var theProp in propInfos)
            {
                viewData[theProp.Name] = baseClass.BaseDo(theProp.GetValue(options).ToString());
            }
        }

        /// <summary>
        /// Base on type for option config, use the correct type key to bind it.
        /// </summary>
        /// <typeparam name="T">Options type</typeparam>
        /// <param name="config">Standard IConfig</param>
        /// <param name="optionKey">String representing Option class identifier</param>
        /// <returns></returns>
        public static T OptionBinder<T>(this IConfiguration config, string optionKey)
            where T: new()
        {
            var options = new T();

            config.GetSection(optionKey).Bind(options);

            return options;
        }
    }
}