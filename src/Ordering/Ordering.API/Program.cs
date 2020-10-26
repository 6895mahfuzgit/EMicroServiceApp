using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Data;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateAndSeedData(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateAndSeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var orderContext = services.GetRequiredService<OrderContext>();
                    OrderContextSeed.SeedAsync(orderContext, logFactory);
                }
                catch (Exception ex)
                {
                    var log = logFactory.CreateLogger<Program>();

                    log.LogError(ex.Message);

                }
            }
        }



    }
}
