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
//using DiabetesOnContainer.DTOs.FichePatient;

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

//        // GET: api/Consultations
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Consultation_Read>>> GetConsultations()
//        {
//            if (_context.Consultations == null)
//            {
//                return NotFound();
//            }
//            var consult = await _context.Consultations.Include(rq => rq.Question)
//                    .ToListAsync();
//            return Ok(_mapper.Map<IEnumerable<Consultation_Read>>(consult));
//        }

//        // GET: api/Consultations/5
//        [HttpGet("{Prescription_ID}")]
//        public async Task<ActionResult<IEnumerable<Consultation_Read>>> GetConsultation(int Prescription_ID)
//        {
//            if (_context.Consultations == null)
//            {
//                return NotFound();
//            }
//            var consultation = await _context.Consultations
//                .Include(rq => rq.Question)
//                .Where(con => con.PrescriptionId == Prescription_ID)
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
//            var consult = await _context.Consultations
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
//            if (_context.Consultations == null)
//            {
//                return Problem("Entity set 'DiabetesOnContainersContext.Consultations'  is null.");
//            }
//            if (await ConsultationExists(consultation.PrescriptionId, consultation.QuestionId))
//            {
//                return Conflict("the consultation for the given question and prescription are already exists try to change the question ");
//            }
//            var consult = _mapper.Map<Consultation>(consultation);
//            await _context.Consultations.AddAsync(consult);

//            await _context.SaveChangesAsync();


//            return CreatedAtAction(nameof(GetConsultation), new { Prescription_ID = consultation.PrescriptionId }, consultation);
//        }

//        // DELETE: api/Consultations/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteConsultation(int id)
//        {
//            if (_context.Consultations == null)
//            {
//                return NotFound();
//            }
//            var consultation = await _context.Consultations.Where(req => req.PrescriptionId == id).ToListAsync();
//            if (consultation == null)
//            {
//                return NotFound();
//            }

//            _context.Consultations.RemoveRange(consultation);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private async Task<bool> ConsultationExists(int P_ID, int Q_ID)
//        {
//            return await _context.Consultations.AnyAsync(e => e.PrescriptionId == P_ID && e.QuestionId == Q_ID);
//        }
//    }
//}
