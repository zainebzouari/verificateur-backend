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
    public class FamillesController : ControllerBase
    {
        private readonly dbVerificateurContext _context;

        public FamillesController(dbVerificateurContext context)
        {
            _context = context;
        }

        // GET: api/Familles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Famille>>> GetFamilles()
        {
          if (_context.Familles == null)
          {
              return NotFound();
          }
            return await _context.Familles.ToListAsync();
        }

        // GET: api/Familles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Famille>> GetFamille(string id)
        {
          if (_context.Familles == null)
          {
              return NotFound();
          }
            var famille = await _context.Familles.FindAsync(id);

            if (famille == null)
            {
                return NotFound();
            }

            return famille;
        }

        // PUT: api/Familles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamille(string id, Famille famille)
        {
            if (id != famille.IdFamille)
            {
                return BadRequest();
            }

            _context.Entry(famille).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilleExists(id))
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

        // POST: api/Familles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Famille>> PostFamille(Famille famille)
        {
          if (_context.Familles == null)
          {
              return Problem("Entity set 'dbVerificateurContext.Familles'  is null.");
          }
            _context.Familles.Add(famille);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FamilleExists(famille.IdFamille))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFamille", new { id = famille.IdFamille }, famille);
        }

        // DELETE: api/Familles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamille(string id)
        {
            if (_context.Familles == null)
            {
                return NotFound();
            }
            var famille = await _context.Familles.FindAsync(id);
            if (famille == null)
            {
                return NotFound();
            }

            _context.Familles.Remove(famille);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamilleExists(string id)
        {
            return (_context.Familles?.Any(e => e.IdFamille == id)).GetValueOrDefault();
        }
    }
}
