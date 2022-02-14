using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       var isDocker = Environment.GetEnvironmentVariable("ISDOCKER");

                       config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile($"ocelot{(!String.IsNullOrEmpty(isDocker) ? ".docker" : "")}.json")
                            .AddEnvironmentVariables();
                   });
                });
    }
}
