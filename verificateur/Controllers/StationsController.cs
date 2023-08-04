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
    public class StationsController : ControllerBase
    {
        private readonly dbVerificateurContext _context;

        public StationsController(dbVerificateurContext context)
        {
            _context = context;
        }

        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
          if (_context.Stations == null)
          {
              return NotFound();
          }
            return await _context.Stations.ToListAsync();
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Station>> GetStation(string id)
        {
          if (_context.Stations == null)
          {
              return NotFound();
          }
            var station = await _context.Stations.FindAsync(id);

            if (station == null)
            {
                return NotFound();
            }

            return station;
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStation(string id, Station station)
        {
            if (id != station.IdStation)
            {
                return BadRequest();
            }

            _context.Entry(station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
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

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(Station station)
        {
          if (_context.Stations == null)
          {
              return Problem("Entity set 'dbVerificateurContext.Stations'  is null.");
          }
            _context.Stations.Add(station);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StationExists(station.IdStation))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStation", new { id = station.IdStation }, station);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(string id)
        {
            if (_context.Stations == null)
            {
                return NotFound();
            }
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationExists(string id)
        {
            return (_context.Stations?.Any(e => e.IdStation == id)).GetValueOrDefault();
        }
    }
}
