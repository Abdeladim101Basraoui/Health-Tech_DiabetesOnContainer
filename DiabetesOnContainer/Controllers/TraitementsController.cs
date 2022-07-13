using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using AutoMapper;
using DiabetesOnContainer.DTOs.FicheMed;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc,Assist")]
    public class TraitementsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public TraitementsController(DiabetesOnContainersContext context,IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Traitements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Traitement_READ>>> GetTraitements()
        {
            if (_context.Traitements== null)
            {
                return NotFound();
            }
            return await _context.Traitements
                .ProjectTo<Traitement_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Traitements/5
        [HttpGet("{Id}")]   
        public async Task<ActionResult<IEnumerable<Traitement_READ>>> GetTraitement(int Id)
        {
            if (_context.FicheMedicals.Find(Id) == null)
            {
                return NotFound("Exmaen ne trouve pas");
            }

            if (!TraitementExists(Id))
                return NotFound($"ce paramBio d''examen num :' >>> {Id} <<< ' n''existe pas");

            var fiche = await _context.Traitements
                .Where(req => req.FicheMedId== Id)
                .ProjectTo<Traitement_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (fiche == null)
            {
                return NotFound();
            }

            return fiche;
        }


        [HttpGet("{ficheId}/{Id}")]
        public async Task<ActionResult<Traitement_READ>> GetTraitementById(int ficheId, int Id)
        {
            if (_context.FicheMedicals.Find(ficheId) == null)
            {
                return NotFound("the Examen Medical does not exists");
            }
            var trait = _mapper.Map<Traitement_READ>(TraitementExistsUP(ficheId, Id).Result);

            if (trait == null)
            {
                return NotFound();
            }

            return trait;
        }



        // PUT: api/Traitements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Change/{ficheId}/{Id}")]
        public async Task<IActionResult> PutTrait(int ficheId, int Id, Traitement_CUD update)
        {

            var fiche = TraitementExistsUP(ficheId, Id).Result;

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
            return AcceptedAtAction(nameof(GetTraitementById), new { ficheId, Id }, fiche);
        }


        [HttpPatch]
        public async Task<IActionResult> TraitementPatch(int ficheId, int Id, [FromBody] JsonPatchDocument<Traitement_READ> update)
        {

            try
            {


                var fiche = await _context.Traitements
                    .Where(con => con.FicheMedId == ficheId && con.TraitId== Id)
                    .ProjectTo<Traitement_READ>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (fiche == null)
                {
                    return NotFound("the Param  >> " + ficheId + " <<   does not exists");
                }

                update.ApplyTo(fiche);

                var value = _mapper.Map<Traitement>(fiche);

                _context.Entry(value).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetTraitementById), new { ficheId,  Id }, fiche);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }




        // POST: api/Traitements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Traitement_READ>> PostAnalysis(Traitement_CUD data)
        {
            if (_context.Traitements == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.ExamainMedicals'  is null.");
            }
            var trait = _mapper.Map<Traitement>(data);

            _context.Traitements.Add(trait);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTraitementById), new {ficheId =  trait.FicheMedId, Id = trait.TraitId }, data);
        }

        // DELETE: api/Traitements/5
        [HttpDelete("Delete/{ficheId}")]
        public async Task<IActionResult> DeleteTraitByFiche(int ficheId)
        {
            if (_context.Traitements == null)
            {
                return NotFound();
            }

            var fiche = await _context.Traitements
                .Where(con => con.FicheMedId == ficheId)
                .ToListAsync();
            if (fiche == null)
            {
                return NotFound();
            }

            _context.Traitements.RemoveRange(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("Delete/{ficheId}/{Id}")]
        public async Task<IActionResult> DeleteEchographieById(int ficheId, int Id)
        {
            var fiche = TraitementExistsUP(ficheId, Id).Result;
            if (fiche == null)
            {
                return NotFound();
            }

            _context.Traitements.Remove(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraitementExists(int id)
        {
            return (_context.Traitements?.Any(e => e.FicheMedId== id)).GetValueOrDefault();
        }

        private async Task<Traitement> TraitementExistsUP(int ficheId, int Id)
        {
            var row = await _context.Traitements
                    .FirstOrDefaultAsync(req => req.FicheMedId == ficheId && req.TraitId== Id);
            return row;
        }
    }
}
