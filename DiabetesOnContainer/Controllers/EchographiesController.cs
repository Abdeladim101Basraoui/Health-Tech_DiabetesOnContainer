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
//    public class EchographiesController : ControllerBase
//    {
//        private readonly DiabetesOnContainersContext _context;
//        private readonly IMapper _mapper;

//        public EchographiesController(DiabetesOnContainersContext context, IMapper mapper)
//        {
//            _context = context;
//            this._mapper = mapper;
//        }

//        //GET: api/Echographies
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Echographie_READ>>> GetEchographies()
//        {
//            if (_context.Echographies == null)
//            {
//                return NotFound();
//            }
//            return await _context.Echographies
//                .ProjectTo<Echographie_READ>(_mapper.ConfigurationProvider)
//                .ToListAsync();
//        }

//        //GET: api/Echographies/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Echographie_READ>> GetEchographie(int id)
//        {
//            if (_context.Echographies == null)
//            {
//                return NotFound();
//            }
//            var echographie = await _context.Echographies
//                .ProjectTo<Echographie_READ>(_mapper.ConfigurationProvider)
//                .FirstOrDefaultAsync(req => req.EchographieId == id);

//            if (echographie == null)
//            {
//                return NotFound();
//            }

//            return echographie;
//        }

//        //PUT: api/Echographies/5
//        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEchographie(int id, Echographie_CUD request)
//        {
//            var echographie = await _context.Echographies
//                                .FindAsync(id);

//            if (echographie == null)
//            {
//                return NotFound();
//            }

//            _mapper.Map(request, echographie);
//            _context.Entry(echographie).State = EntityState.Modified;


//            return NoContent();
//        }

//        //POST: api/Echographies
//        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Echographie_CUD>> PostEchographie(Echographie_CUD request)
//        {
//            if (_context.Echographies == null)
//            {
//                return Problem("Entity set 'DiabetesOnContainersContext.Echographies'  is null.");
//            }

//            var echographie = _mapper.Map<Echographie>(request);
//            await _context.Echographies.AddAsync(echographie);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetEchographie), new { id = echographie.EchographieId }, echographie);
//        }

//        //DELETE: api/Echographies/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEchographie(int id)
//        {
//            if (_context.Echographies == null)
//            {
//                return NotFound();
//            }
//            var echographie = await _context.Echographies.FindAsync(id);
//            if (echographie == null)
//            {
//                return NotFound();
//            }

//            _context.Echographies.Remove(echographie);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EchographieExists(int id)
//        {
//            return (_context.Echographies?.Any(e => e.EchographieId == id)).GetValueOrDefault();
//        }
//    }
//}
