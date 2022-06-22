using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Logic;
using ef_core_example.Models;
using Microsoft.AspNetCore.Mvc;

namespace ef_core_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : MarketplaceController
    {
        private readonly IProfileLogic _logic;

        public ProfileController(IProfileLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => 
            await _logic.GetProfile(id)
                    .Finally(ToActionResult);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MarketplaceProfile profileDto) =>
            await _logic.CreateProfile(profileDto)
                    .Finally(ToActionResult);

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MarketplaceProfile profileDto) =>
            await _logic.UpdateProfile(profileDto)
                .Finally(ToActionResult);
    }
}