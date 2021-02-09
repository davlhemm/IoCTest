using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IoCTest;
using IoCTest.Interfaces;
using IoCTest.Model;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IoCWebAppAspCore
{
    public static class StartupConfig
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IBase, FurtherDerived>();

            //TODO: Pull config items here?!  How do we normally do this?! 
            var fileLoggerOptions = Configuration.OptionBinder<FileLoggerOptions>(FileLoggerOptions.FileLogger);
            //TODO: derive different filename here but use filename as a base from options json
            services.AddSingleton<ILogger>(x => 
                new FileLogger($"{fileLoggerOptions.FileName}{DateTime.Now:yyyyMMdd-HHmmss}{fileLoggerOptions.Extension}"));

            var backupOptions = Configuration.OptionBinder<BackupOptions>(BackupOptions.Backup);
            services.AddSingleton<IBackup>(x => new ZipBackup());

            return services;
        }
    }
}
