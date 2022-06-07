using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ef_core_example.Logic;
using ef_core_example.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProfileLogic _logic;

        public Worker(ILogger<Worker> logger, IProfileLogic logic)
        {
            _logger = logger;
            _logic = logic;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    // var id1 = Guid.Parse("0155c260-e10e-4be5-19c3-08d98c18362b");
                    // var id2 = Guid.Parse("6558ab54-d78a-404b-754d-08d9747e07f8");

                    var orders = await _logic.GetOldOrders();

                    int count = 0;

                    if (orders.IsSuccess)
                        count = orders.Value.Count();

                    // if (count == 2)
                    // {
                    //     profile = await _logic.GetProfile(id2);
                    // }
                    // count++;
                    _logger.LogInformation("Worker running at: {time} for depot: {depotName}", DateTimeOffset.Now, count);
                    await Task.Delay(10000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
