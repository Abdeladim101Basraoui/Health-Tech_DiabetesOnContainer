using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using DiabetesOnContainer.DTOs.GestionPatient;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class complicationsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public complicationsController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }


        // GET: api/CasComplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complication_Read>>> GetCasComplications()
        {
            if (_context.CasComplications == null)
            {
                return NotFound();
            }
            //Ok(_mapper.Map<IEnumerable<Complication_CR>>(await _context.CasComplications.ToListAsync()));
            return Ok(await _context.CasComplications
                            //.Include(fk => fk.Patient)
                            .ProjectTo<Complication_Read>(_mapper.ConfigurationProvider)
                            .ToListAsync()
                     );
        }


        // GET: api/CasComplications/pt1234
        //give all the complications for the CIN given
        [HttpGet("{cin}")]
        public async Task<ActionResult<IEnumerable<Complication_Read>>> GetCasComplication(string cin)
        {

            if (_context.CasComplications == null || !CasComplicationExists(cin))
            {
                return NotFound();
            }

            return await _context.CasComplications
                        //.Include(fk => fk.Patient)
                        .Where(fk => fk.PatientId == cin)
                        .ProjectTo<Complication_Read>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }



        [HttpGet("{cin}/HisId")]
        public async Task<ActionResult<Complication_Read>> GetCasComplicationById(string cin, int CompId)
        {
            if (_context.CasComplications== null || _context.Patients.Find(cin) == null)
            {
                return NotFound("the aptient does not exists");
            }
            var compli = _mapper.Map<Complication_Read>(ComplicationExistsUP(CompId, cin).Result);

            if (compli == null)
            {
                return NotFound("the patient does not the historique   >>>" +CompId);
            }

            return compli;

        }
        // PUT: api/CasComplications/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cin}/{CompId}")]
        public async Task<IActionResult> PutCasComplication(string cin, int CompId, Complication_CUD update)
        {
            var complication = ComplicationExistsUP(CompId, cin).Result;

            if (complication == null || update.PatientId != cin)
            {
                return NotFound();
            }

            //map the values comming as DTO class to the Model class

            _mapper.Map(update, complication);

            //send the model data to be modified
            _context.Entry(complication).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPatch("{Cin}/{Id}")]
        public async Task<IActionResult> PatcComplication(string Cin, int Id, [FromBody] JsonPatchDocument<Complication_Read> update)
        {
            try
            {
                var compli = await _context.CasComplications
                        .Include(fk => fk.Patient)
                        .Where(con => con.PatientId == Cin && con.ComplicationId == Id)
                        .ProjectTo<Complication_Read>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
                ;
                if (compli == null)
                {
                    return NotFound("the cin does not exists in the table");
                }
                update.ApplyTo(compli);
                var value = _mapper.Map<CasComplication>(compli);

                _context.Entry(value).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetCasComplicationById), new { cin = Cin, CompId = Id }, compli);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }


        // POST: api/CasComplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add/")]
        public async Task<ActionResult<Complication_CUD>> PostCasComplication(Complication_CUD complication)
        {
            if (_context.CasComplications == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.CasComplications'  is null.");
            }

            if (complication == null)
            {
                return BadRequest();
            }

            var row = await _context.CasComplications
                      .FirstOrDefaultAsync(con => con.PatientId == complication.PatientId);

            if (row == null) return NotFound("the patient does not exists");

            var casComplication = _mapper.Map<CasComplication>(complication);


            await _context.CasComplications
                .AddAsync(casComplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCasComplication), new { cin = casComplication.PatientId }, complication);
        }


        // DELETE: api/FichePatients/5
        [HttpDelete("delete/{Cin}")]
        public async Task<IActionResult> DeleteComplicationByCIN(string Cin)
        {
            if (_context.CasComplications == null)
            {
                return NotFound();
            }
            var complications = await _context.CasComplications
                .Where(con => con.PatientId == Cin)
                .ToListAsync();

            if (complications == null)
            {
                return NotFound();
            }

            _context.CasComplications.RemoveRange(complications);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FichePatients/5
        [HttpPost("delete/{Cin}/{PresId}")]
        public async Task<IActionResult> DeleteComplicationByID(string Cin, int PresId)
        {
            var complication = _mapper.Map<CasComplication>(ComplicationExistsUP(PresId, Cin).Result);
            if (complication == null)
            {
                return NotFound();
            }

            _context.CasComplications.Remove(complication);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool CasComplicationExists(string cin)
        {
            return (_context.CasComplications?.Any(e => e.PatientId == cin)).GetValueOrDefault();
        }
        private async Task<CasComplication> ComplicationExistsUP(int CompId, string Cin)
        {
            var row = await _context.CasComplications
                .Where(con => con.PatientId == Cin && con.ComplicationId == CompId)
                  .FirstOrDefaultAsync();
            return row;
        }
    }
}
