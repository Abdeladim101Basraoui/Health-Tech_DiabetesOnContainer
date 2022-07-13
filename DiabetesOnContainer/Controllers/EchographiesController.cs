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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doc,Assist")]
    public class EchographiesController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public EchographiesController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        //GET: api/Echographies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Echographie_READ>>> GetEchographies()
        {
            if (_context.Echographies == null)
            {
                return NotFound();
            }
            return await _context.Echographies
                .ProjectTo<Echographie_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        //GET: api/Echographies/5
        [HttpGet("{ExamId}")]
        public async Task<ActionResult<IEnumerable<Echographie_READ>>> GetEchographieByExam(int ExamId)
        {
            if (_context.ExamainMedicals.Find(ExamId) == null)
            {
                return NotFound("Exmaen ne trouve pas");
            }

            if (!EchographieExists(ExamId))
                return NotFound($"ce paramBio d''examen num :' >>> {ExamId} <<< ' n''existe pas");

            var Echog = await _context.Echographies
                .Where(req => req.ExamainId == ExamId)
                .ProjectTo<Echographie_READ>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (Echog == null)
            {
                return NotFound();
            }

            return Echog;
        }


        [HttpGet("{ExamId}/{EchoId}")]
        public async Task<ActionResult<Echographie_READ>> GetEchographieById(int ExamId, int EchoId)
        {
            if (_context.ExamainMedicals.Find(ExamId) == null)
            {
                return NotFound("the Examen Medical does not exists");
            }
            var echog = _mapper.Map<Echographie_READ>(EchogExistsUP(ExamId, EchoId).Result);

            if (echog == null)
            {
                return NotFound();
            }

            return echog;
        }


        //PUT: api/Echographies/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Change/{ExamId}/{EchoId}")]
        public async Task<IActionResult> PutEchographie(int ExamId, int EchoId, Echographie_CD update)
        {

            var Echog = EchogExistsUP(ExamId, EchoId).Result;

            if (Echog == null)
            {
                return NotFound();
            }

            //map the values comming as DTO class to the Model class

            _mapper.Map(update, Echog);

            //send the model data to be modified
            _context.Entry(Echog).State = EntityState.Added;

            await _context.SaveChangesAsync();

            //return NoContent();
            return AcceptedAtAction(nameof(GetEchographieById),new {ExamenId = ExamId,EchoId }, Echog);
        }

        [HttpPatch("Update/{ExamId}/{EchoId}")]
        public async Task<IActionResult> EchographiePatch(int ExamId, int EchoId, [FromBody] JsonPatchDocument<Echographie_READ> update)
        {

            try
            {

                var Echog = await _context.Echographies
                    .Where(con => con.EchographieId== EchoId && con.ExamainId == ExamId)
                    .ProjectTo<Echographie_READ>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (Echog == null)
                {
                    return NotFound("the Param  >> " + ExamId + " <<   does not exists");
                }

                update.ApplyTo(Echog);

                var value = _mapper.Map<Echography>(Echog);

                _context.Entry(value).State = EntityState.Modified;
                //_context.Entry(value).CurrentValues.SetValues(_context.Echographies);

                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetEchographieById), new { ExamId , EchoId }, Echog);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }

        //POST: api/Echographies
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("New")]
        public async Task<ActionResult<Echographie_READ>> PostEchographie(Echographie_CD echographie)
        {
            if (_context.Echographies==null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.ExamainMedicals'  is null.");
            }
            var Echog = _mapper.Map<Echography>(echographie);

            _context.Echographies.Add(Echog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEchographieByExam), new { ExamId = echographie.ExamainId }, echographie);
        }

        //DELETE: api/Echographies/5
        [HttpDelete("Delete/{ExamId}")]
        public async Task<IActionResult> DeleteEchographieByExam(int ExamId)
        {
            if (_context.ParamsBios == null)
            {
                return NotFound();
            }

            var Echog = await _context.Echographies
                .Where(con => con.ExamainId == ExamId)
                .ToListAsync();
            if (Echog == null)
            {
                return NotFound();
            }

            _context.Echographies.RemoveRange(Echog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Delete/{ExamId}/{EchogId}")]
        public async Task<IActionResult> DeleteEchographieById(int ExamId, int EchogId)
        {
            var Echog = EchogExistsUP(ExamId, EchogId).Result;
            if (Echog == null)
            {
                return NotFound();
            }

            _context.Echographies.Remove(Echog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EchographieExists(int Id)
        {
            return (_context.Echographies?.Any(e => e.ExamainId == Id)) is not null;
        }

        private async Task<Echography> EchogExistsUP(int ExamId, int EchoId)
        {
            var row = await _context.Echographies
                    .FirstOrDefaultAsync(req => req.ExamainId == ExamId && req.EchographieId == EchoId);
            return row;
        }
    }

}
