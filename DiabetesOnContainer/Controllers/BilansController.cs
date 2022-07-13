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
using DiabetesOnContainer.DTOs.FicheMed;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc,Assist")]
    public class BilansController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public BilansController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Bilans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bilan_READ>>> GetBilans()
        {
            if (_context.Bilans == null)
            {
                return NotFound();
            }
            return await _context.Bilans
                .ProjectTo<Bilan_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Bilans/5
        [HttpGet("{ficheId}")]
        public async Task<ActionResult<Bilan_READ>> GetBilan(int ficheId)
        {
            if (_context.Bilans == null)
            {
                return NotFound();
            }
            if (!BilanExists(ficheId))
                return NotFound($"ce analyse d''examen num :' >>> {ficheId} <<< ' n''existe pas");

            var bilan = await _context.Bilans
                .ProjectTo<Bilan_READ>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(req => req.FicheMedId == ficheId);

            if (bilan == null)
            {
                return NotFound();
            }

            return bilan;
        }

        // PUT: api/Bilans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Change/{ficheId}/{Id}")]
        public async Task<IActionResult> PutAnalysis(int ficheId, int Id, Bilan_CUD update)
        {

            var fiche = BilanExistsUP(ficheId, Id).Result;

            if (fiche == null)
            {
                return NotFound();
            }

            //map the values comming as DTO class to the Model class

            _mapper.Map(update, fiche);

            //send the model data to be modified
            _context.Entry(fiche).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            //return NoContent();
            return AcceptedAtAction(nameof(GetBilanById), new { ficheId, bilanId = Id }, fiche);
        }

        [HttpGet("{ficheId}/{bilanId}")]
        public async Task<ActionResult<Bilan_READ>> GetBilanById(int ficheId, int bilanId)
        {
            if (_context.FicheMedicals.Find(ficheId) == null)
            {
                return NotFound("the Examen Medical does not exists");
            }
            var bilan = _mapper.Map<Bilan_READ>(BilanExistsUP(ficheId, bilanId).Result);

            if (bilan == null)
            {
                return NotFound();
            }

            return bilan;
        }


        [HttpPatch]
        public async Task<IActionResult> BilanPatch(int ficheId, int Id, [FromBody] JsonPatchDocument<Bilan_READ> update)
        {

            try
            {


                var fiche = await _context.Bilans
                    .Where(con => con.FicheMedId == ficheId && con.BilanId == Id)
                    .ProjectTo<Bilan_READ>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (fiche == null)
                {
                    return NotFound("the Param  >> " + ficheId + " <<   does not exists");
                }

                update.ApplyTo(fiche);

                var value = _mapper.Map<Bilan>(fiche);

                _context.Entry(value).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetBilanById), new { ficheId, bilanId = Id }, fiche);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }

        // POST: api/Bilans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bilan_CUD>> PostBilan(Bilan_CUD data)
        {
            if (_context.Bilans == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.ExamainMedicals'  is null.");
            }
            var bilans = _mapper.Map<Bilan>(data);

            _context.Bilans.Add(bilans);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBilanById), new { ficheId = bilans.FicheMedId, bilanId = bilans.BilanId }, data);
        }

        // DELETE: api/Bilans/5
        [HttpDelete("Delete/{ficheId}")]
        public async Task<IActionResult> DeleteBilanByFiche(int ficheId)
        {
            if (_context.Bilans == null)
            {
                return NotFound();
            }

            var fiche = await _context.Bilans
                .Where(con => con.FicheMedId == ficheId)
                .ToListAsync();
            if (fiche == null)
            {
                return NotFound();
            }

            _context.Bilans.RemoveRange(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Delete/{ficheId}/{Id}")]
        public async Task<IActionResult> DeleteBilanById(int ficheId, int Id)
        {
            var fiche = BilanExistsUP(ficheId, Id).Result;
            if (fiche == null)
            {
                return NotFound();
            }

            _context.Bilans.Remove(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool BilanExists(int ficheId)
        {
            return (_context.Bilans?.Any(e => e.FicheMedId == ficheId)).GetValueOrDefault();
        }


        private async Task<Bilan> BilanExistsUP(int ficheId, int bilanId)
        {
            var row = await _context.Bilans
                    .FirstOrDefaultAsync(req => req.FicheMedId == ficheId && req.BilanId == bilanId);
            return row;
        }

    }
}
