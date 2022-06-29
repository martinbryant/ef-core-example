using ef_core_example;
using ef_core_example.Logic;
using GoldMarketplace.ServerAPIService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, builder) =>
                {
                    var file = hostingContext.Configuration["Logging:DataLogger:Filepath"];
                    builder.AddFile(file, isJson: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var Configuration = hostContext.Configuration;

                    services.AddDbContext<AppDbContext>(
                        options =>
                        {
                            var conn = Configuration["ConnectionStrings:DefaultConnection"];
                            options.UseMySql(conn, new MariaDbServerVersion(new System.Version(10,0)));
                            options.EnableSensitiveDataLogging();
                        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                    services.AddTransient<IProfileLogic, ProfileLogic>();
                    services.AddTransient<IDepotLogic, DepotLogic>();
                    services.AddHostedService<Worker>();
                });
    }
}
