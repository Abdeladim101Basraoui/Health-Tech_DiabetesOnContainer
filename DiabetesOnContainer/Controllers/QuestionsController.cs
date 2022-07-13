using System;
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
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc")]
    public class QuestionsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public QuestionsController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question_READ>>> GetQuestions()
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            return await _context.Questions
                .ProjectTo<Question_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question_READ>> GetQuestion(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var question = await _context.Questions
                .ProjectTo<Question_READ>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(con => con.QuestionId == id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question_CUD Request)
        {

            var question = await _context.Questions.FindAsync(id);

            if (question == null) { return NotFound(); }

            _mapper.Map(Request, question);
            _context.Entry(question).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question_CUD>> PostQuestion(Question_CUD request)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.Questions'  is null.");
            }

            var question = _mapper.Map<Question>(request);
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, request);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return (_context.Questions?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
