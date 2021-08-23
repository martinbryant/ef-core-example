using System;
using System.Linq;
using ef_core_example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ef_core_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepotController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DepotController> _logger;

        public DepotController(AppDbContext context, ILogger<DepotController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var depot = _context.Depots.FirstOrDefault(depot => depot.Id == id);
            return Ok(depot);
        }

        public IActionResult Post([FromBody] Depot depot)
        {
            _context.Depots.Add(depot);
            _context.SaveChanges();

            return Created($"{Request.Path}/{depot.Id}", depot);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Depot depot)
        {
            _context.Depots.Update(depot);

            // depot.DeliveryAddress = address;

            _context.SaveChangesAsync();

            return Ok(depot);
        }
    }
}
