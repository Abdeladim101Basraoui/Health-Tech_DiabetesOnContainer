﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using DiabetesOnContainer.DTOs.GestionPatient;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichePatientsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public FichePatientsController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/FichePatients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichePatient_Read>>> GetFichePatients()
        {
            if (_context.FichePatients == null)
            {
                return NotFound();
            }
            return await _context.FichePatients
                .Include(con => con.Consultations)
                .ThenInclude(fk=>fk.Question)
                .ProjectTo<FichePatient_Read>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/FichePatients/bs8623
        [HttpGet("{cin}")]
        public async Task<ActionResult<IEnumerable<FichePatient_Read>>> GetFichePatientByCIN(string cin)
        {
            if (_context.FichePatients == null || _context.Patients.Find(cin) == null)
            {
                return NotFound("the aptient does not exists");
            }

            else if (!FichePatientExists(cin)) return NotFound("ce patient n'a aucun historique");
            return await _context.FichePatients
                .Include(con => con.Consultations)
                .ThenInclude(fk => fk.Question)
                .Where(fk => fk.Cin == cin)
                                   .ProjectTo<FichePatient_Read>(_mapper.ConfigurationProvider)
                                   .ToListAsync();
        }

        // GET: api/FichePatients/5
        [HttpGet("{cin}/{PrescriptionID}")]
        public async Task<ActionResult<FichePatient_Read>> GetFichePatientByID(string cin, int PrescriptionID)
        {
            if (_context.FichePatients == null || _context.Patients.Find(cin) == null)
            {
                return NotFound("the patient does not exists");
            }
            var fichePatient = _mapper.Map<FichePatient_Read>(FichePatientExistsUP(PrescriptionID, cin).Result);

            if (fichePatient == null)
            {
                return NotFound();
            }

            return fichePatient;
        }


        // PUT: api/FichePatients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Cin}/{PreId}")]
        public async Task<IActionResult> PutFichePatient(string Cin, int PreId, FichePatient_Create update)
        {
            var fiche = FichePatientExistsUP(PreId, Cin).Result;

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

        //Patch: api/FichePatients/5/12
        [HttpPatch("/{Cin}/{PreId}")]
        public async Task<IActionResult> PatchFilePatient(string Cin,int PreId, [FromBody] JsonPatchDocument<FichePatient_Patch> update)
        {

            try
            {

                if (_context.Patients.Find(Cin) == null)
                {
                    return NotFound("the patient does not exist");
                }

                var fiche = FicheExistsPatch(Cin, PreId).Result;

                if (fiche == null)
                {
                    return NotFound("la fiche"+PreId+ "does not exists");
                }
                update.ApplyTo(fiche);
                var value = _mapper.Map<FichePatient>(fiche);

                _context.Entry(value).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetFichePatientByID), new { cin = Cin, PrescriptionID = PreId }, fiche);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }


        // POST: api/FichePatients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FichePatient_Create>> PostFichePatient(FichePatient_Create data)
        {
            if (_context.FichePatients == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.FichePatients'  is null.");
            }
            var fichePatient = _mapper.Map<FichePatient>(data);
            await _context.FichePatients.AddAsync(fichePatient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFichePatientByID), new { cin = fichePatient.Cin, PrescriptionID = fichePatient.PrescriptionId }, data);
        }

        // DELETE: api/FichePatients/5
        [HttpDelete("{Cin}")]
        public async Task<IActionResult> DeleteFichePatientByCIN(string Cin)
        {
            if (_context.FichePatients == null)
            {
                return NotFound();
            }
            var fichePatient = await _context.FichePatients
                .Where(con => con.Cin == Cin)
                .ToListAsync();
            if (fichePatient == null)
            {
                return NotFound();
            }

            _context.FichePatients.RemoveRange(fichePatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FichePatients/5
        [HttpDelete("{Cin}/{PresId}")]
        public async Task<IActionResult> DeleteFichePatientByID(string Cin, int PresId)
        {
            var fichePatient = FichePatientExistsUP(PresId, Cin).Result;
            if (fichePatient == null)
            {
                return NotFound();
            }

            _context.FichePatients.Remove(fichePatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<FichePatient> FichePatientExistsUP(int Id, string Cin)
        {
            var row = await _context.FichePatients
                      .Include(con => con.Consultations)
                .ThenInclude(fk => fk.Question)
                    .FirstOrDefaultAsync(req => req.Cin == Cin && req.PrescriptionId == Id);
            return row;
        }
        private bool FichePatientExists(string cin)
        {
            return _context.FichePatients?.Any(e => e.Cin == cin) is not null;
        }

        private async Task<FichePatient_Patch> FicheExistsPatch(string cin, int FichId)
        {
            var row = await _context.FichePatients
                        //.Include(fk => fk.Consultations)
                        //.ThenInclude(fk=>fk.Question)
                        .Where(con => con.Cin == cin && con.PrescriptionId == FichId)
                        .ProjectTo<FichePatient_Patch>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();

            return row;
        }
    }
}
