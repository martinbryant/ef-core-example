using System;
using System.Linq;
using System.Threading.Tasks;
using ef_core_example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get(Guid id)
        {
            var profile = await _context.Profiles
                .Include(p => p.Depots)
                .FirstOrDefaultAsync(profile => profile.Id == id);

            return Ok(profile);
        }

        public IActionResult Post([FromBody] Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();

            return Created($"{Request.Path}/{profile.Id}", profile);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;

            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
            
            _context.SaveChanges();

            return Ok(profile);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var profile = _context.Profiles.FirstOrDefault(profile => profile.Id == id);
            _context.Profiles.Remove(profile);
            
            _context.SaveChangesAsync();

            return Ok();
        }
    }
}
