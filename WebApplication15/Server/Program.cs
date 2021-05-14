using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication15.Server
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
                    webBuilder.UseStartup<Startup>();


                    if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PORT")))
                    {
                        string dynoport = Environment.GetEnvironmentVariable("PORT");
                        string useUrl = $"http://*:{dynoport}";
                        webBuilder.UseUrls(useUrl);
                    }
                    else
                    {
                        Console.WriteLine("No PORT Env Var is found");
                    }

                });
    }
}
