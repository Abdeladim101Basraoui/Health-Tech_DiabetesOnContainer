//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using DiabetesOnContainer.Models;
//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using DiabetesOnContainer.DTOs.GestionPatient;

//namespace DiabetesOnContainer.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ConsultationsController : ControllerBase
//    {
//        private readonly DiabetesOnContainersContext _context;
//        private readonly IMapper _mapper;

//        public ConsultationsController(DiabetesOnContainersContext context, IMapper mapper)
//        {
//            _context = context;
//            this._mapper = mapper;
//        }


//        // GET: api/Consultations/5
//        [HttpGet("{Prescription_ID}")]
//        public async Task<ActionResult<IEnumerable<Consultation_Read>>> GetConsultation(int Prescription_ID)
//        {
//            var consultation = await _context.FichePatients
//                .Where(con => con.PrescriptionId == Prescription_ID)
//                .Include(rq => rq.Question)
//                .Include(q => q.Prescription)
//                .ToListAsync();
//            if (consultation == null)
//            {
//                return NotFound();
//            }
//            var consult = _mapper.Map<IEnumerable<Consultation_Read>>(consultation);

//            return Ok(consult);
//        }

//        // PUT: api/Consultations/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{Prescription_id}")]
//        public async Task<IActionResult> PutConsultation(int Prescription_id, int Question_ID, Consultation_update consultation)
//        {
//            if (!await ConsultationExists(Prescription_id, Question_ID))
//            {
//                return NotFound("the consultation with does cridential does not exists");
//            }
//            var consult = await _context.Consultation
//                         .FirstOrDefaultAsync(req => req.PrescriptionId == Prescription_id && req.QuestionId == Question_ID);

//            if (consult == null)
//            {
//                return NotFound();
//            }
//            _mapper.Map(consultation, consult);
//            _context.Entry(consult).State = EntityState.Modified;


//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // POST: api/Consultations
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Consultation_Create>> PostConsultation(Consultation_Create consultation)
//        {
//            if (_context.Consultation == null)
//            {
//                return Problem("Entity set 'DiabetesOnContainersContext.Consultation'  is null.");
//            }
//            if (await ConsultationExists(consultation.PrescriptionId, consultation.QuestionId))
//            {
//                return Conflict("the consultation for the given question and prescription are already exists try to change the question ");
//            }
//            var consult = _mapper.Map<Consultation>(consultation);
//            await _context.Consultation.AddAsync(consult);

//            await _context.SaveChangesAsync();


//            return CreatedAtAction(nameof(GetConsultation), new { Prescription_ID = consultation.PrescriptionId }, consultation);
//        }

//        // DELETE: api/Consultations/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteConsultation(int id)
//        {
//            if (_context.Consultation == null)
//            {
//                return NotFound();
//            }
//            var consultation = await _context.Consultation.Where(req => req.PrescriptionId == id).ToListAsync();
//            if (consultation == null)
//            {
//                return NotFound();
//            }

//            _context.Consultation.RemoveRange(consultation);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private async Task<bool> ConsultationExists(int P_ID, int Q_ID)
//        {
//            return await _context.Consultation.AnyAsync(e => e.PrescriptionId == P_ID && e.QuestionId == Q_ID);
//        }
//    }
//}
