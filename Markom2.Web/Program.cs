using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
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
                    var loggerConfig = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File(new JsonFormatter(), "log/log.json")
                        .CreateLogger();

                    log.AddSerilog(loggerConfig);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
