using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetWebAPI.Model;

namespace dotnetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FicheMedicalsController : ControllerBase
    {
        private readonly DataContext _context;

        public FicheMedicalsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FicheMedicals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FicheMedical>>> GetficheMedicals()
        {
          if (_context.ficheMedicals == null)
          {
              return NotFound();
          }
            return await _context.ficheMedicals.ToListAsync();
        }

        // GET: api/FicheMedicals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FicheMedical>> GetFicheMedical(string id)
        {
          if (_context.ficheMedicals == null)
          {
              return NotFound();
          }
            var ficheMedical = await _context.ficheMedicals.FindAsync(id);

            if (ficheMedical == null)
            {
                return NotFound();
            }

            return ficheMedical;
        }

        // PUT: api/FicheMedicals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFicheMedical(string id, FicheMedical ficheMedical)
        {
            if (id != ficheMedical.ID)
            {
                return BadRequest();
            }

            _context.Entry(ficheMedical).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FicheMedicalExists(id))
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

        // POST: api/FicheMedicals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FicheMedical>> PostFicheMedical(FicheMedical ficheMedical)
        {
          if (_context.ficheMedicals == null)
          {
              return Problem("Entity set 'DataContext.ficheMedicals'  is null.");
          }
            _context.ficheMedicals.Add(ficheMedical);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FicheMedicalExists(ficheMedical.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFicheMedical", new { id = ficheMedical.ID }, ficheMedical);
        }

        // DELETE: api/FicheMedicals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFicheMedical(string id)
        {
            if (_context.ficheMedicals == null)
            {
                return NotFound();
            }
            var ficheMedical = await _context.ficheMedicals.FindAsync(id);
            if (ficheMedical == null)
            {
                return NotFound();
            }

            _context.ficheMedicals.Remove(ficheMedical);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FicheMedicalExists(string id)
        {
            return (_context.ficheMedicals?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
