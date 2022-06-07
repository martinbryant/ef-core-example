using ef_core_example.Logic;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ef_core_example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static ILoggerFactory DataLogger { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .ConfigureApiBehaviorOptions(
                        options =>
                        {
                            options.InvalidModelStateResponseFactory =  // the interjection
                                ModelStateValidator.ValidateModelState;
                        });

            //             DataLogger = LoggerFactory.Create(builder =>
            //             {
            //                 builder.AddFile(Configuration["Logging:DataLogger:Filepath"],
            // isJson: true,
            // outputTemplate: "{Timestamp:o} {RequestId,8} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}");
            //             });

            services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);
                    options.EnableSensitiveDataLogging();
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProfileLogic, ProfileLogic>();
            services.AddScoped<IDepotLogic, DepotLogic>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            // app.UseRequestResponseLogger(logger);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
