using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TORWebAPIDemo.Model;

namespace TORWebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    public class CncMembersController : ControllerBase
    {
        private readonly TorDbContext _context;

        public CncMembersController(TorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<CncMember> GetCncMembers()
        {
            return _context.CncMembers;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCncMember([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cncMember = await _context.CncMembers.SingleOrDefaultAsync(m => m.Id == id);

            if (cncMember == null)
            {
                return NotFound();
            }
            return Ok(cncMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCncMember([FromRoute] int id, [FromBody] CncMember cncMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cncMember.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(cncMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CncMemberExists(id))
                {
                    return NotFound();
                }
                throw;
                
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostCncMember([FromBody] CncMember cncMember)
        {
            if (!ModelState.IsValid || CncMemberExists(cncMember.Id))
            {
                return BadRequest(ModelState);
            }

            _context.CncMembers.Add(cncMember);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCncMember", new {id = cncMember.Id}, cncMember );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCncMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.CncMembers.SingleOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.CncMembers.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok(todo);
        }
        
        private bool CncMemberExists(int id)
        {
            return _context.CncMembers.Any(c => c.Id == id);
        }
    }
}