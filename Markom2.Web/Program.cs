using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Json;

namespace Markom2.Web
{
    public class Program
    {
        public static void Main(string[] args) =>        
            CreateHostBuilder(args).Build().Run();
        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(log =>
                {
                    var providers = new LoggerProviderCollection();
                    var loggerConfig = new LoggerConfiguration()
                        .WriteTo.Providers(providers)
                        .WriteTo.Console()
                        .WriteTo.File(new JsonFormatter(), "log/log.json")
                        .CreateLogger();

                    log.ClearProviders(); // agar provider default tidak di ikut sertakan
                    log.AddSerilog(loggerConfig);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
