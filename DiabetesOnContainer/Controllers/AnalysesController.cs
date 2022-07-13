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
    [Authorize(Roles ="Doc,Assist")]
    public class AnalysesController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public AnalysesController(DiabetesOnContainersContext context,IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Analyses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Analysis_READ>>> GetAnalyses()
        {
          if (_context.Analyses == null)
          {
              return NotFound();
          }
            return await _context.Analyses
                .ProjectTo<Analysis_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/Analyses/5
        [HttpGet("{ficheId}")]
        public async Task<ActionResult<IEnumerable<Analysis_READ>>> GetAnalysis(int ficheId)
        {
            if (_context.FicheMedicals.Find(ficheId) == null)
            {
                return NotFound("fiche ce ne trouve pas");
            }

            if (!ficheExists(ficheId))
                return NotFound($"ce analyse d''examen num :' >>> {ficheId} <<< ' n''existe pas");

            var fiche = await _context.Analyses
                .Where(req => req.FicheMedId== ficheId)
                .ProjectTo<Analysis_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (fiche == null)
            {
                return NotFound();
            }

            return fiche;
        }



        [HttpGet("{ficheId}/{analyseId}")]
        public async Task<ActionResult<Analysis_READ>> GetAnalysisById(int ficheId, int analyseId)
        {
            if (_context.FicheMedicals.Find(ficheId) == null)
            {
                return NotFound("the Examen Medical does not exists");
            }
            var analyse = _mapper.Map<Analysis_READ>(AnalysisExistsUP(ficheId, analyseId).Result);

            if (analyse == null)
            {
                return NotFound();
            }

            return analyse;
        }



        [HttpPatch]
        public async Task<IActionResult> AnalysisPatch(int ficheId, int Id, [FromBody] JsonPatchDocument<Analysis_READ> update)
        {

            try
            {


                var fiche = await _context.Analyses
                    .Where(con => con.FicheMedId == ficheId && con.AnalyseId== Id)
                    .ProjectTo<Analysis_READ>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (fiche == null)
                {
                    return NotFound("the Param  >> " + ficheId + " <<   does not exists");
                }

                update.ApplyTo(fiche);

                var value = _mapper.Map<Analysis>(fiche);

                _context.Entry(value).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetAnalysisById), new {ficheId,analyseId = Id}, fiche);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }





        // PUT: api/Analyses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Change/{ficheId}/{Id}")]
        public async Task<IActionResult> PutAnalysis(int ficheId, int Id, Analysis_CUD update)
        {

            var fiche = AnalysisExistsUP(ficheId, Id).Result;

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
            return AcceptedAtAction(nameof(GetAnalysisById), new { ficheId,analyseId =Id}, fiche);
        }

        // POST: api/Analyses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Analysis_READ>> PostAnalysis(Analysis_CUD data)
        {
            if (_context.Analyses == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.ExamainMedicals'  is null.");
            }
            var analysis = _mapper.Map<Analysis>(data);

            _context.Analyses.Add(analysis);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnalysisById), new { ficheId = analysis.FicheMedId,analyseId = analysis.AnalyseId}, data);
        }

        // DELETE: api/Analyses/5
        [HttpDelete("Delete/{ficheId}")]
        public async Task<IActionResult> DeleteEchographieByFiche(int ficheId)
        {
            if (_context.Analyses== null)
            {
                return NotFound();
            }

            var fiche = await _context.Analyses
                .Where(con => con.FicheMedId== ficheId)
                .ToListAsync();
            if (fiche == null)
            {
                return NotFound();
            }

            _context.Analyses.RemoveRange(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("Delete/{ficheId}/{Id}")]
        public async Task<IActionResult> DeleteEchographieById(int ficheId, int Id)
        {
            var fiche = AnalysisExistsUP(ficheId, Id).Result;
            if (fiche == null)
            {
                return NotFound();
            }

            _context.Analyses.Remove(fiche);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ficheExists(int ficheid)
        {
            return (_context.Analyses?.Any(e => e.FicheMedId == ficheid)).GetValueOrDefault();
        }


        private async Task<Analysis> AnalysisExistsUP(int ficheId, int analysisId)
        {
            var row = await _context.Analyses
                    .FirstOrDefaultAsync(req => req.FicheMedId == ficheId && req.AnalyseId == analysisId);
            return row;
        }
    }
}
