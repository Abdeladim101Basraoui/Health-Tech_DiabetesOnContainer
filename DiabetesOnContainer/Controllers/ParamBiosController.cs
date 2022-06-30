//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using DiabetesOnContainer.Models;
//using AutoMapper;
//using DiabetesOnContainer.DTOs.ExamainMedical.Diagnostics;
//using AutoMapper.QueryableExtensions;

//namespace DiabetesOnContainer.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ParamBiosController : ControllerBase
//    {
//        private readonly DiabetesOnContainersContext _context;
//        private readonly IMapper _mapper;

//        public ParamBiosController(DiabetesOnContainersContext context, IMapper mapper)
//        {
//            _context = context;
//            this._mapper = mapper;
//        }

//        // GET: api/ParamBios
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ParamBio_READ>>> GetParamBios()
//        {
//            if (_context.ParamBios == null)
//            {
//                return NotFound();
//            }
//            return await _context.ParamBios
//                .ProjectTo<ParamBio_READ>(_mapper.ConfigurationProvider)
//                .ToListAsync();
//        }

//        // GET: api/ParamBios/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ParamBio_READ>> GetParamBio(int id)
//        {
//            if (_context.ParamBios == null)
//            {
//                return NotFound();
//            }
//            var paramBio = await _context.ParamBios
//                .ProjectTo<ParamBio_READ>(_mapper.ConfigurationProvider)
//                .FirstOrDefaultAsync(req => req.ParamBioId == id);

//            if (paramBio == null)
//            {
//                return NotFound();
//            }

//            return paramBio;
//        }

//        // PUT: api/ParamBios/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutParamBio(int id, ParamBio_CUD request)
//        {
//            var paramBio = await _context.ParamBios
//                                    .FindAsync(id);

//            if (paramBio == null) return NotFound();
//            _mapper.Map(request, paramBio);


//            _context.Entry(paramBio).State = EntityState.Modified;

//            await _context.SaveChangesAsync();


//            return NoContent();
//        }

//        // POST: api/ParamBios
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<ParamBio_CUD>> PostParamBio(ParamBio_CUD request)
//        {
//            if (_context.ParamBios == null)
//            {
//                return Problem("Entity set 'DiabetesOnContainersContext.ParamBios'  is null.");
//            }

//            var paramBio = _mapper.Map<ParamBio>(request);
//            _context.ParamBios.Add(paramBio);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetParamBio), new { id = paramBio.ParamBioId }, paramBio);
//        }

//        // DELETE: api/ParamBios/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteParamBio(int id)
//        {
//            if (_context.ParamBios == null)
//            {
//                return NotFound();
//            }
//            var paramBio = await _context.ParamBios.FindAsync(id);
//            if (paramBio == null)
//            {
//                return NotFound();
//            }

//            _context.ParamBios.Remove(paramBio);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ParamBioExists(int id)
//        {
//            return (_context.ParamBios?.Any(e => e.ParamBioId == id)).GetValueOrDefault();
//        }
//    }
//}
