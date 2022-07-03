using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using DiabetesOnContainer.DTOs.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DiabeticiensController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public DiabeticiensController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }



        // GET: api/Diabeticiens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiabeticienREAD>>> GetDiabeticiens()
        {

            if (_context.Diabeticiens == null)
            {
                return NotFound();
            }
            //return Ok(_mapper.Map<IEnumerable<DiabeticienREAD>>
            //                (await _context.Diabeticiens.ToListAsync())
            //         );

            return Ok(await _context.Diabeticiens
                                            .ProjectTo<DiabeticienREAD>(_mapper.ConfigurationProvider)
                                            .ToListAsync());
        }



        // GET: api/Diabeticiens/med1234
        [HttpGet("{cin}")]

        public async Task<ActionResult<DiabeticienREAD>> GetDiabeticien(string cin)
        {
            if (_context.Diabeticiens == null)
            {
                return NotFound();
            }

            //var diabeticien = await _context.Diabeticiens.FindAsync(id);
            var diabeticien = await _context.Diabeticiens
                                    .ProjectTo<DiabeticienREAD>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync(q => q.Cin == cin);
            if (diabeticien == null)
            {
                return NotFound();
            }

            return Ok(diabeticien);
        }




        // PUT: api/Diabeticiens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cin}")]
        public async Task<IActionResult> PutDiabeticien(string cin, DiabeticienUpdate diabeticienDRUD)
        {
            if (!DiabeticienExists(cin))
            {
                return NotFound();
            }
            var casRow = await _context.Diabeticiens.FindAsync(cin);

            _mapper.Map(diabeticienDRUD, casRow);

            _context.Entry(casRow).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return NoContent();
        }



        // POST: api/Diabeticiens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiabeticienCD>> PostDiabeticien(DiabeticienCD diabeticien)
        {

            if (_context.Diabeticiens == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.Diabeticiens'  is null.");
            }

            var Dupdate = _mapper.Map<Diabeticien>(diabeticien);

            await _context.Diabeticiens.AddAsync(Dupdate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiabeticienExists(diabeticien.Cin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetDiabeticien), new { cin = diabeticien.Cin }, diabeticien);
        }

        //patch api/assistant/as1234
        [HttpPatch("{Cin}")]
        public async Task<IActionResult> PatchDiabeticien(string Cin, [FromBody] JsonPatchDocument<DiabeticienCD> update)
        {
            var doc = AssistExistsPatch(Cin).Result;
            if (doc == null)
            {
                return NotFound("the cin does not exists in the table");
            }
            update.ApplyTo(doc);
            var value = _mapper.Map<Diabeticien>(doc);

            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return AcceptedAtAction(nameof(GetDiabeticien), new { cin = Cin }, doc);
        }


        // DELETE: api/Diabeticiens/5
        [HttpDelete("{cin}")]
        public async Task<IActionResult> DeleteDiabeticien(string cin)
        {
            if (_context.Diabeticiens == null)
            {
                return NotFound();
            }
            var diabeticien = await _context.Diabeticiens.FindAsync(cin);
            if (diabeticien == null)
            {
                return NotFound();
            }

            _context.Diabeticiens.Remove(diabeticien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiabeticienExists(string cin)
        {
            return (_context.Diabeticiens?.Any(e => e.Cin == cin)).GetValueOrDefault();
        }
        private async Task<DiabeticienCD> AssistExistsPatch(string cin)
        {
            var row = await _context.Diabeticiens
                        .ProjectTo<DiabeticienCD>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync(req => req.Cin == cin);

            return row;
        }

    }
}
