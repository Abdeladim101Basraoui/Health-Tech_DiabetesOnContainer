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

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Bilan_READ>> GetBilan(int id)
        {
            if (_context.Bilans == null)
            {
                return NotFound();
            }
            var bilan = await _context.Bilans
                .ProjectTo<Bilan_READ>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(req => req.BilanId == id);

            if (bilan == null)
            {
                return NotFound();
            }

            return bilan;
        }

        // PUT: api/Bilans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilan(int id, Bilan_CUD Request)
        {
            if (!BilanExists(id))
            {
                return NotFound();
            }
            var bilan = await _context.Bilans.FindAsync(id);
            if (bilan == null) return NotFound("the bilan with the id sent does not exists");

            _mapper.Map(Request, bilan);
            _context.Entry(bilan).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Bilans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bilan_CUD>> PostBilan(Bilan_CUD request)
        {
            if (_context.Bilans == null)
            {
                return Problem("Entity set 'DiabetesOnContainersContext.Bilans'  is null.");
            }
            var bilan = _mapper.Map<Bilan>(request);
            _context.Bilans.Add(bilan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBilan), new { id = bilan.BilanId }, bilan);
        }

        // DELETE: api/Bilans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilan(int id)
        {
            if (_context.Bilans == null)
            {
                return NotFound();
            }
            var bilan = await _context.Bilans.FindAsync(id);
            if (bilan == null)
            {
                return NotFound();
            }

            _context.Bilans.Remove(bilan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BilanExists(int id)
        {
            return (_context.Bilans?.Any(e => e.BilanId == id)).GetValueOrDefault();
        }
    }
}
