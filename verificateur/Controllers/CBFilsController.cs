using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using verificateur.Models;

namespace verificateur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CBFilsController : ControllerBase
    {
        private readonly dbVerificateurContext _context;

        public CBFilsController(dbVerificateurContext context)
        {
            _context = context;
        }

        // GET: api/CBFils
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CBFil>>> GetCBFils()
        {
            Console.WriteLine("Enter username:", _context);

            if (_context.CBFils == null)
          {
              return NotFound();
          }
            return await _context.CBFils.ToListAsync();
        }

        // GET: api/CBFils/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CBFil>> GetCBFil(string id)
        {
          if (_context.CBFils == null)
          {
              return NotFound();
          }
            var cBFil = await _context.CBFils.FindAsync(id);

            if (cBFil == null)
            {
                return NotFound();
            }

            return cBFil;
        }

        // PUT: api/CBFils/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCBFil(string id, CBFil cBFil)
        {
            if (id != cBFil.IdFils)
            {
                return BadRequest();
            }

            _context.Entry(cBFil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CBFilExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CBFils
// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
[HttpPost]
public async Task<ActionResult<CBFil>> PostCBFil(CBFil cBFil)
{
    if (cBFil == null)
    {
        return BadRequest("CBFil data is null.");
    }

    // Assuming the IdFils is a string. If not, modify the condition accordingly.
    if (string.IsNullOrEmpty(cBFil.IdFils))
    {
        return BadRequest("IdFils cannot be null or empty.");
    }

    _context.CBFils.Add(cBFil);
    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateException)
    {
        if (CBFilExists(cBFil.IdFils))
        {
            return Conflict("CBFil with the same IdFils already exists.");
        }
        else
        {
            throw;
        }
    }

    // Use CreatedAtRoute instead of CreatedAtAction for consistency
    return CreatedAtAction("GetCBFil", new { id = cBFil.IdFils }, cBFil);
}


        // DELETE: api/CBFils/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCBFil(string id)
        {
            if (_context.CBFils == null)
            {
                return NotFound();
            }
            var cBFil = await _context.CBFils.FindAsync(id);
            if (cBFil == null)
            {
                return NotFound();
            }

            _context.CBFils.Remove(cBFil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CBFilExists(string id)
        {
            return (_context.CBFils?.Any(e => e.IdFils == id)).GetValueOrDefault();
        }
    }
}
