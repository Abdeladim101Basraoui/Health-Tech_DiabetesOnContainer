using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using AutoMapper;
using DiabetesOnContainer.DTOs.FichePatient;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc,Assist")]
    public class HistoriquesController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public HistoriquesController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Historiques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historique_READ>>> GetHistoriques()
        {
            if (_context.Historiques == null)
            {
                return NotFound();
            }
            return await _context.Historiques
                .ProjectTo<Historique_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Historiques/pt1234
        [HttpGet("{cin}")]
        public async Task<ActionResult<IEnumerable<Historique_READ>>> GetHistoriqueByCIN(string cin)
        {
            if (_context.Historiques == null || _context.Patients.Find(cin) == null)
            {
                return NotFound("the aptient does not exists");
            }
            else if (!HistoriqueExists(cin)) return NotFound("ce patient n'a aucun historique");
            return await _context.Historiques
                   .Where(fk => fk.PatientId == cin)
                                   .ProjectTo<Historique_READ>(_mapper.ConfigurationProvider)
                                   .ToListAsync();

        }


        // GET: api/Historiques/pt1234
        [HttpGet("{cin}/HisId")]
        public async Task<ActionResult<Historique_READ>> GetHistoriqueById(string cin, int HisId)
        {
            if (_context.Historiques == null || _context.Patients.Find(cin) == null)
            {
                return NotFound("the aptient does not exists");
            }
            var historique = _mapper.Map<Historique_READ>(HistoriqueExistsUP(HisId, cin).Result);

            if (historique == null)
            {
                return NotFound("the patient does not the historique " + HisId);
            }

            return historique;

        }



        // PUT: api/Historiques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cin}/{HisId}")]
        public async Task<ActionResult<Historique_READ>> PutHistorique(string cin, int HisId, Historique_CUD update)
        {
            var historique = HistoriqueExistsUP(HisId, cin).Result;

            if (historique == null)
            {
                return NotFound();
            }

            //map the values comming as DTO class to the Model class

            _mapper.Map(update, historique);

            //send the model data to be modified
            _context.Entry(historique).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/CasComplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add/")]
        public async Task<ActionResult<Historique_CUD>> PostHistorique( Historique_CUD update)
        {
            if (_context.Historiques == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.CasComplications'  is null.");
            }

            if (update == null || update.PatientId != update.PatientId)
            {
                return BadRequest("check the values send");
            }
            var historique = _mapper.Map<Historique>(update);


                await _context.Historiques
                .AddAsync(historique);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistoriqueExists(historique.PatientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetHistoriqueByCIN), new { cin = historique.PatientId }, historique);
        }


        [HttpPatch("{Cin}/{Id}")]
        public async Task<IActionResult> PatchHistorique(string Cin, int Id, [FromBody] JsonPatchDocument<Historique_READ> update)
        {
            try
            {
                var historique = HistoriqueExistsPatch(Cin,Id).Result;
                if (historique == null)
                {
                    return NotFound("the cin does not exists in the table");
                }

                update.ApplyTo(historique);
                var value = _mapper.Map<Historique>(historique);

                _context.Entry(value).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetHistoriqueById), new { cin = Cin, HisId = Id }, historique);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }




        // DELETE: api/historique/pt12345
        [HttpDelete("delete/{Cin}")]
        public async Task<IActionResult> DeleteHistoriqueByCIN(string Cin)
        {
            if (_context.Historiques == null)
            {
                return NotFound();
            }
            var historique = await _context.Historiques
                .Where(con => con.PatientId == Cin)
                .ToListAsync();

            if (historique == null)
            {
                return NotFound();
            }

            _context.Historiques.RemoveRange(historique);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        //this delete action method is not Idempotent for this reson I will change the status verb to Post to follow the respt Spec
        // DELETE: api/historique/pt1234/5
        [HttpPost("delete/{Cin}/{PresId}")]
        public async Task<IActionResult> DeleteHistoriqueByID(string Cin, int PresId)
        {
            var historique = HistoriqueExistsUP(PresId, Cin).Result;
            if (historique == null)
            {
                return NotFound();
            }

            _context.Historiques.Remove(historique);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool HistoriqueExists(string cin)
        {
            return _context.Historiques?.Any(e => e.PatientId == cin) is not null;
        }

        private async Task<Historique> HistoriqueExistsUP(int HisId, string Cin)
        {
            var row = await _context.Historiques
                .Where(con => con.PatientId == Cin && con.HistoriqueId == HisId)
                  .FirstOrDefaultAsync();
            return row;
        }

        private async Task<Historique_READ> HistoriqueExistsPatch(string cin,int HisId)
        {
            var row = await _context.Historiques
                        .Include(fk=>fk.Patient)
                        .Where(con=>con.PatientId == cin && con.HistoriqueId ==  HisId)
                        .ProjectTo<Historique_READ>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();

            return row;
        }

    }
}
