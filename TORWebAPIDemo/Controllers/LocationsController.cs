using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TORWebAPIDemo.Model;

namespace TORWebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly TorDbContext _context;

        public LocationsController(TorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }
        
        [HttpGet("{id}/parent")]
        public async Task<IActionResult> GetParentLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return RedirectToAction("GetLocation", "Locations" , new { id = location.ParentLocationId });
        }
    }
}