﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using AutoMapper;
using DiabetesOnContainer.DTOs.GestionPatient;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc,Assist")]
    public class ExamainMedicalsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public ExamainMedicalsController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/ExamainMedicals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenMed_Read>>> GetExamainMedicals()
        {
            if (_context.ExamainMedicals == null)
            {
                return NotFound();
            }
            return await _context.ExamainMedicals
                .Include(k=>k.Echographies)
                .Include(k=>k.ParamsBios)
                .ProjectTo<ExamenMed_Read>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/ExamainMedicals/5
        [HttpGet("{PresId}")]
        public async Task<ActionResult<IEnumerable<ExamenMed_Read>>> GetExamainMedical(int PresId)
        {
            if (_context.ExamainMedicals == null || _context.FichePatients.Find(PresId) == null)
            {
                return NotFound("cette fiche n'existe pas");
            }

            if (!ExamExists(PresId)) return NotFound("ce examen >> "+PresId+" << n'exist pas");
            return await _context.ExamainMedicals
                .Include(k => k.Echographies)
                .Include(k => k.ParamsBios)
                .Where(con => con.PrescriptionId == PresId)
                .ProjectTo<ExamenMed_Read>(_mapper.ConfigurationProvider)
                                   .ToListAsync();
        }

        // GET: api/exmaen/5/1
        [HttpGet("{PresId}/{ExamId}")]
        public async Task<ActionResult<ExamenMed_Read>> GetExamtByID(int PresId, int ExamId)
        {
            if ( _context.FichePatients.Find(PresId) == null)
            {
                return NotFound("the fiche Patient does not exists");
            }
            var fichePatient = _mapper.Map<ExamenMed_Read>(ExamExistsUP(PresId, ExamId).Result);

            if (fichePatient == null)
            {
                return NotFound();
            }

            return fichePatient;
        }


        // PUT: api/ExamainMedicals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{PresId}/{ExamId}")]
        public async Task<IActionResult> PutExamainMedical(int PresId, int ExamId, ExamenMed_Update update)
        {

            var fiche = ExamExistsUP(PresId, ExamId).Result;

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


///TODO : the patch not working
//Patch: api/FichePatients/5/12
        [HttpPatch("update/{PresId}/{ExamId}")]
        public async Task<IActionResult> ExamPatch(int PresId, int ExamId, [FromBody] JsonPatchDocument<ExamenMed_Patch> update)
        {

            try
            {

                if (_context.ExamainMedicals.Find(ExamId) == null)
                {
                    return NotFound("the fiche Patient does not exist");
                }

                var fiche = _mapper.Map<ExamenMed_Patch>(ExamExistsUP(PresId, ExamId).Result);
                if (fiche == null)
                {
                    return NotFound("la fiche  >> " + ExamId + " <<   does not exists");
                }

                update.ApplyTo(fiche);

                _context.Entry(_mapper.Map<ExamainMedical>(fiche)).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetExamtByID), new { PresId, ExamId }, fiche);

            }
            catch (Exception ex)                
            {

                return Content(ex.Message);
            }
        }


        // POST: api/ExamainMedicals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("New")]
        public async Task<ActionResult<ExamenMed_Read>> PostExamainMedical(ExamenMed_CD examainMedical)
        {
            if (_context.ExamainMedicals == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.ExamainMedicals'  is null.");
            }
            var exam = _mapper.Map<ExamainMedical>(examainMedical);
            _context.ExamainMedicals.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExamainMedical), new { PresId = examainMedical.PrescriptionId }, examainMedical);
        }

        // DELETE: api/ExamainMedicals/5
        [HttpDelete("Delete/{PresId}")]
        public async Task<IActionResult> DeleteFichePatientByCIN(int PresId)
        {
            if (_context.ExamainMedicals == null)
            {
                return NotFound();
            }
            var fichePatient = await _context.ExamainMedicals
                .Where(con => con.PrescriptionId == PresId)
                .ToListAsync();
            if (fichePatient == null)
            {
                return NotFound();
            }

            _context.ExamainMedicals.RemoveRange(fichePatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("Delete/{PresId}/{ExamId}")]
        public async Task<IActionResult> DeleteFichePatientByID(int PresId, int ExamId)
        {
            var fichePatient = ExamExistsUP(PresId, ExamId).Result;
            if (fichePatient == null)
            {
                return NotFound();
            }

            _context.ExamainMedicals.Remove(fichePatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamExists(int PresId)
        {
            return _context.ExamainMedicals?.Any(e => e.PrescriptionId == PresId) is not null;
        }
        private async Task<ExamainMedical> ExamExistsUP(int PresId, int ExamId)
        {
            var row = await _context.ExamainMedicals
                    .FirstOrDefaultAsync(req => req.PrescriptionId == PresId && req.ExamainId == ExamId);
            return row;
        }



    }
}
