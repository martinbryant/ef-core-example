using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Logic;
using ef_core_example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ef_core_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepotController : MarketplaceController
    {
        private readonly IDepotLogic _logic;

        public DepotController(IDepotLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) =>
            await _logic.GetDepot(id)
                    .Finally(ToActionResult);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepotDto depotDto) =>
            await _logic.CreateDepot(depotDto)
                    .Finally(ToActionResult);

        // [HttpPut]
        // public async Task<IActionResult> Update([FromBody] DepotDto depotDto) =>
        //     await _logic.UpdateDepot(depotDto)
        //             .Finally(ToActionResult);

        
    }
}
