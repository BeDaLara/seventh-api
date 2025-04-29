using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeventhGearApi.Models;

namespace SeventhGearApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly SeventhGearDbContext _context;

        public ConfigurationsController(SeventhGearDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Configuration>> PostConfiguration(Configuration configuration)
        {
            configuration.CreatedAt = DateTime.UtcNow;
            _context.Configurations.Add(configuration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguration", new { id = configuration.Id }, configuration);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Configuration>> GetConfiguration(int id)
        {
            var configuration = await _context.Configurations.FindAsync(id);

            if (configuration == null)
            {
                return NotFound();
            }

            return configuration;
        }
    }
}