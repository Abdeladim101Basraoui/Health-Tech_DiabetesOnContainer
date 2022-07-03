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
    public class FichePatientsController : ControllerBase
    {
        private readonly DataContext _context;

        public FichePatientsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FichePatients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichePatient>>> GetPatients()
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            using (_context)
            {
                FichePatient fp = new FichePatient("abdeladim","basraoui");
                _context.Patients.Add(fp);
                _context.SaveChanges();
                
            }
                return await _context.Patients.ToListAsync();
        }

        // GET: api/FichePatients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FichePatient>> GetFichePatient(int id)
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            var fichePatient = await _context.Patients.FindAsync(id);

            if (fichePatient == null)
            {
                return NotFound();
            }

            return fichePatient;
        }

        // PUT: api/FichePatients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichePatient(int id, FichePatient fichePatient)
        {
            if (id != fichePatient.C_ID)
            {
                return BadRequest();
            }

            _context.Entry(fichePatient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichePatientExists(id))
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

        // POST: api/FichePatients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FichePatient>> PostFichePatient(FichePatient fichePatient)
        {
          if (_context.Patients == null)
          {
              return Problem("Entity set 'DataContext.Patients'  is null.");
          }
            _context.Patients.Add(fichePatient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFichePatient", new { id = fichePatient.C_ID }, fichePatient);
        }

        // DELETE: api/FichePatients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFichePatient(int id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var fichePatient = await _context.Patients.FindAsync(id);
            if (fichePatient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(fichePatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichePatientExists(int id)
        {
            return (_context.Patients?.Any(e => e.C_ID == id)).GetValueOrDefault();
        }
    }
}
