using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DiabetesOnContainer.DTOs.GestionPatient;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc,Assist")]
    public class PatientsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public PatientsController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient_READ>>> GetPatients()
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<Patient_READ>>(await _context.Patients.ToListAsync()));
        }

        


        // GET: api/Patients/pat1234
        [HttpGet("{cin}")]
        public async Task<ActionResult<Patient_READ>> GetPatient(string cin)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients
                .ProjectTo<Patient_READ>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Cin == cin);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/pat1234
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cin}")]
        public async Task<IActionResult> PutPatient(string cin, PatientUpdate patient)
        {
            if (!PatientExists(cin))
            {
                return NotFound();
            }

            var row = await _context.Patients
                        .FindAsync(cin);

            _mapper.Map(patient, row);

            _context.Entry(row).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/Patients1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient_READ>> PostPatient(Patient_CUD newPatient)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.Patients'  is null.");
            }

            var patient = _mapper.Map<Patient>(newPatient);

            await _context.Patients
                 .AddAsync(patient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.Cin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetPatient), new { cin = newPatient.Cin }, newPatient);
        }

        // DELETE: api/Patients1/med1234
        [HttpDelete("{cin}")]
        public async Task<IActionResult> DeletePatient(string cin)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(cin);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{Cin}")]
        public async Task<IActionResult> PatchAssistant(string Cin, [FromBody] JsonPatchDocument<Patient_CUD> update)
        {
            var patient = PatientExistsPatch(Cin).Result;
            if (patient == null)
            {
                return NotFound("the cin does not exists in the table");
            }
            update.ApplyTo(patient);
            var value = _mapper.Map<Patient>(patient);

            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return AcceptedAtAction(nameof(GetPatient), new { cin = Cin }, patient);
        }



        private bool PatientExists(string id)
        {
            return (_context.Patients?.Any(e => e.Cin == id)).GetValueOrDefault();
        }

        private async Task<Patient_CUD> PatientExistsPatch(string cin)
        {
            var row = await _context.Patients
                        .ProjectTo<Patient_CUD>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync(req => req.Cin == cin);

            return row;
        }

    }
}
