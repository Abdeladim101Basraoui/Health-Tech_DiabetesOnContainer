using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using AutoMapper;
using DiabetesOnContainer.DTOs.FicheMed;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FicheMedicalsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public FicheMedicalsController(DiabetesOnContainersContext context,IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/FicheMedicals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FicheMedical_READ>>> GetFicheMedicals()
        {
          if (_context.FicheMedicals == null)
          {
              return NotFound();
          }
            return await _context.FicheMedicals
                .Include(f=>f.Analyses)
                .Include(k=>k.Bilans)
                .Include(fk=>fk.Traitements)
                .ProjectTo<FicheMedical_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/FicheMedicals/5
        [HttpGet("{cin}")]
        public async Task<ActionResult<IEnumerable<FicheMedical_READ>>> GetFicheMedical(string cin)
        {
          if (_context.FicheMedicals == null )
          {
              return NotFound("the table is empty");
          }
            if (!FicheMedicalExists(cin))
            {
                return NotFound("cette fiche n'a aucaun existant");
            }

            return await _context.FicheMedicals
           .Where(fk => fk.PatientId == cin)
           .Include(f => f.Analyses)
           .Include(k => k.Bilans)
           .Include(fk => fk.Traitements)
           .ProjectTo<FicheMedical_READ>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        [HttpGet("{cin}/{ficheId}")]
        public async Task<ActionResult<FicheMedical_READ>> GetFicheMedicalByID(string cin, int ficheId)
        {

            if (_context.FichePatients == null)
            {
                return NotFound("the table is EMPTY");
            }

            if (!FicheMedicalExists(cin)) return NotFound("ce patient n'a aucun historique");

            var fichePatient = _mapper.Map<FicheMedical_READ>(FicheMedicalExistsUP(ficheId,cin).Result);

            if (fichePatient == null)
            {
                return NotFound();
            }

            return fichePatient;
        }


        // PUT: api/FicheMedicals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Cin}/{ficheId}")]
        public async Task<IActionResult> PutFicheMedical(string Cin, int ficheId, FicheMedical_CUD update)
        {
            var fiche = FicheMedicalExistsUP(ficheId, Cin).Result;

            if (fiche == null)
            {
                return NotFound();
            }

            //map the values comming as DTO class to the Model class

            _mapper.Map(update, fiche);

            //send the model data to be modified
            _context.Entry(fiche).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FicheMedicals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FicheMedical_CUD>> PostFicheMedical(FicheMedical_CUD data)
        {
            if (_context.FicheMedicals == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.FichePatients'  is null.");
            }
            var fiche = _mapper.Map<FicheMedical>(data);
            await _context.FicheMedicals.AddAsync(fiche);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFicheMedicalByID), new { cin = fiche.PatientId, ficheId = fiche.FicheMedId}, data);
        }


        //Patch: api/FichePatients/5/12
        [HttpPatch("{Cin}/{ficheId}")]
        public async Task<IActionResult> PatchFicheMedical(string Cin, int ficheId, [FromBody] JsonPatchDocument<FicheMedical_CUD> update)
        {
            try
            {
                var exists = FicheMedicalExistsUP(ficheId, Cin).Result;
                var fiche = _mapper.Map<FicheMedical_CUD>(exists);

                if (fiche == null)
                {
                    return NotFound("la fiche" + ficheId + "does not exists");
                }
                update.ApplyTo(fiche);
                _mapper.Map(fiche, exists);
                _context.Entry(exists).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetFicheMedicalByID), new { Cin,ficheId }, fiche);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }


        // DELETE: api/FichePatients/5
        [HttpDelete("{Cin}")]
        public async Task<IActionResult> DeleteFicheMedicalByCIN(string Cin)
        {
            if (_context.FicheMedicals == null)
            {
                return NotFound();
            }
            var fiche = await _context.FicheMedicals
                .Where(con => con.PatientId == Cin)
                .ToListAsync();
            if (fiche == null)
            {
                return NotFound();
            }

            _context.FicheMedicals.RemoveRange(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FichePatients/5
        [HttpPost("{Cin}/{ficheId}")]
        public async Task<IActionResult> DeleteFicheMedicalByID(string Cin, int ficheId)
        {
            var fiche = FicheMedicalExistsUP(ficheId, Cin).Result;
            if (fiche == null)
            {
                return NotFound();
            }

            _context.FicheMedicals.Remove(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool FicheMedicalExists(string cin)
        {
            return (_context.FicheMedicals?.Any(e => e.PatientId== cin)).GetValueOrDefault();
        }

        private async Task<FicheMedical> FicheMedicalExistsUP(int Id, string Cin)
        {
            return  await _context.FicheMedicals
           .Include(f => f.Analyses)
           .Include(k => k.Bilans)
           .Include(fk => fk.Traitements)
           .FirstOrDefaultAsync(fk => fk.PatientId == Cin && fk.FicheMedId == Id);
            
        }
    }
}
