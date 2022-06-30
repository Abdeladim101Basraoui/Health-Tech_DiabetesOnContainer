using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using AutoMapper;
using DiabetesOnContainer.DTOs.Admin;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/admin/[controller]")]

    [ApiController]
    public class AssistantsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public AssistantsController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }


        /// <summary>
        /// GET all the assistants in database
        /// </summary>
        /// <returns></returns>
        // GET: api/Assistants
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AssistREAD>>> GetAssistants()
        {
            if (_context.Assistants == null)
            {
                return NotFound();
            }

            return await _context.Assistants
                //.Select(q=>q.DateNaissance);  --> specify manulaly
                //->use auto mapper

                //association between entities
                //.Include(q => q.)

                .ProjectTo<AssistREAD>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }




        // GET: api/Assistants/assist1234
        [HttpGet("{cin}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AssistREAD>> GetAssistant(string cin)
        {
            if (_context.Assistants == null)
            {
                return NotFound();
            }
            var assistant = await _context.Assistants
                .ProjectTo<AssistREAD>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Cin == cin);

            if (assistant == null)
            {
                return NotFound();
            }

            return assistant;
        }

        // PUT: api/Assistants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cin}")]
        public async Task<IActionResult> PutAssistant(string cin, Assist_Update Update)
        {
            if (!AssistantExists(cin))
            { return NotFound(); }

            var casRow = await _context.Assistants.FindAsync(cin);
            _mapper.Map(Update, casRow);

            _context.Entry(casRow).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }



        // POST: api/Assistants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssistCD>> PostAssistant(AssistCD newOne)
        {
            if (_context.Assistants == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.Assistants'  is null.");
            }

            var assistant = _mapper.Map<Assistant>(newOne);
            await _context.Assistants.AddAsync(assistant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssistantExists(assistant.Cin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetAssistant), new { cin = newOne.Cin }, newOne);
        }

        //patch api/assistant/as1234
        [HttpPatch("{Cin}")]
        public async Task<IActionResult> PatchAssistant(string Cin,[FromBody] JsonPatchDocument<AssistCD> update)
        {
            var Assistant = AssistExistsPatch(Cin).Result;
            if (Assistant == null)
            {
                return NotFound("the cin does not exists in the table");
            }
            update.ApplyTo(Assistant);
            var value = _mapper.Map<Assistant>(Assistant);

            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return AcceptedAtAction(nameof(GetAssistant),new { cin=Cin},Assistant);
        }

        // DELETE: api/Assistants/5
        [HttpDelete("{cin}")]
        public async Task<IActionResult> DeleteAssistant(string cin)
        {
            if (_context.Assistants == null)
            {
                return NotFound();
            }
            var assistant = await _context.Assistants.FindAsync(cin);
            if (assistant == null)
            {
                return NotFound();
            }

            _context.Assistants.Remove(assistant);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool AssistantExists(string cin)
        {
            return (_context.Assistants?.Any(e => e.Cin == cin)).GetValueOrDefault();
        }
        private async Task<AssistCD> AssistExistsPatch(string cin)
        {
            var row = await _context.Assistants
                        .ProjectTo<AssistCD>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync(req=>req.Cin ==  cin);

            return row;
        }
    }
}
