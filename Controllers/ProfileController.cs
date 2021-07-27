using System;
using System.Collections.Generic;
using System.Linq;
using ef_core_example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ef_core_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(AppDbContext context, ILogger<ProfileController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var profile = _context.Profiles.FirstOrDefault(profile => profile.Id == id);
            return Ok(profile);
        }

        public IActionResult Post([FromBody] Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();

            return Created($"{Request.Path}/{profile.Id}", profile);
        }
    }
}
