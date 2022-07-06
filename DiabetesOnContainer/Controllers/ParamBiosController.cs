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

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamBiosController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;

        public ParamBiosController(DiabetesOnContainersContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/ParamBios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParamBio_Read>>> GetParamBios()
        {
            if (_context.ParamsBios == null)
            {
                return NotFound();
            }
            return await _context.ParamsBios
                .ProjectTo<ParamBio_Read>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/ParamBios/5
        [HttpGet("{ExamId}")]
        public async Task<ActionResult<IEnumerable< ParamBio_Read>>> GetParamBioByExam(int ExamId)
        {
            if ( _context.ExamainMedicals.Find(ExamId) == null)
            {
                return NotFound("Exmaen ne trouve pas");
            }

            if (!ParamBioExists(ExamId))
                return NotFound($"ce paramBio d''examen num :' >>> {ExamId} <<< ' n''existe pas");

            var paramBio = await _context.ParamsBios
                .Where(req => req.ExamainId == ExamId)
                .ProjectTo<ParamBio_Read>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (paramBio == null)
            {
                return NotFound();
            }

            return paramBio;
        }

        
        // GET: api/exmaen/5/1
        [HttpGet("{ExamId}/{ParamId}")]
        public async Task<ActionResult<ParamBio_Read>> GetParamBioById( int ExamId, int ParamId)
        {
            if (_context.ExamainMedicals.Find(ExamId) == null)
            {
                return NotFound("the Examen Medical does not exists");
            }
            var Param = _mapper.Map<ParamBio_Read>(ParamBioExistsUP( ExamId,ParamId).Result);

            if (Param == null)
            {
                return NotFound();
            }

            return Param;
        }


        // PUT: api/ParamBios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Change/{ExamId}/{ParamId}")]
        public async Task<IActionResult> PutParamBio( int ExamId, int ParamId, ParamBio_Update update)
        {

            var Param = ParamBioExistsUP( ExamId,ParamId).Result;

            if (Param == null)
            {
                return NotFound();
            }

            //map the values comming as DTO class to the Model class

            _mapper.Map(update, Param);

            //send the model data to be modified
            _context.Entry(Param).State = EntityState.Detached;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("Update/{ExamId}/{ParamId}")]
        public async Task<IActionResult> ParamPatch(int ExamId, int ParamId, [FromBody] JsonPatchDocument<ParamBio_Read> update)
        {

            try
            {

                if (_context.ParamsBios.Find(ParamId) == null)
                {
                    return NotFound("the Param does not exist");
                }

                var Param = await _context.ParamsBios
                    .Where(con => con.ParamBioId == ParamId && con.ExamainId == ExamId)
                    .ProjectTo<ParamBio_Read>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (Param == null)
                {
                    return NotFound("the Param  >> " + ExamId + " <<   does not exists");
                }

                update.ApplyTo(Param);

                var value = _mapper.Map<ParamsBio>(Param);

                _context.Entry(value).State = EntityState.Detached;

                await _context.SaveChangesAsync();

                return AcceptedAtAction(nameof(GetParamBioById), new { ParamId, ExamId }, Param);

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }


        // POST: api/ParamBios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("New")]
        public async Task<ActionResult<ParamBio_Read>> PostParamBio(ParamBio_CD Param)
        {
            if (_context.ParamsBios == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.ExamainMedicals'  is null.");
            }
            var exam = _mapper.Map<ParamsBio>(Param);

            _context.ParamsBios.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParamBioByExam), new { ExamId =  Param.ExamainId}, Param);
        }

        // DELETE: api/ParamBios/5
        [HttpDelete("Delete/{ExamId}")]
        public async Task<IActionResult> DeleteParamBioByExam(int ExamId)
        {
            if (_context.ParamsBios == null)
            {
                return NotFound();
            }
            var Param = await _context.ParamsBios
                .Where(con => con.ExamainId == ExamId)
                .ToListAsync();
            if (Param == null)
            {
                return NotFound();
            }

            _context.ParamsBios.RemoveRange(Param);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("Delete/{ExamId}/{ParamId}")]
        public async Task<IActionResult> DeleteParamBioByID( int ExamId, int ParamId)
        {
            var Param = ParamBioExistsUP(ExamId,ParamId).Result;
            if (Param == null)
            {
                return NotFound();
            }

            _context.ParamsBios.Remove(Param);
            await _context.SaveChangesAsync();

            return NoContent();
        }

 
        private bool ParamBioExists(int Id)
        {
            return (_context.ParamsBios?.Any(e => e.ExamainId == Id)) is not null;
        }

        private async Task<ParamsBio> ParamBioExistsUP( int ExamId,int ParamId)
        {
            var row = await _context.ParamsBios
                    .FirstOrDefaultAsync(req => req.ExamainId == ExamId && req.ParamBioId== ParamId);
            return row;
        }
    }
}
