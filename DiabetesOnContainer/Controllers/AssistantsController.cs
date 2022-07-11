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
using DiabetesOnContainer.DTOs.Admin.log_In_Out;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/admin/[controller]")]

    [ApiController]
    public class AssistantsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AssistantsController(DiabetesOnContainersContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            this._mapper = mapper;
            this._configuration = configuration;
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



        /// <summary>
        ///  GET: api/Assistants/cin
        /// </summary>
        /// <param name="cin"></param>
        /// <returns></returns>
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
        //[HttpPost]
        //public async Task<ActionResult<AssistCD>> PostAssistant(AssistCD newOne)
        //{
        //    if (_context.Assistants == null)
        //    {
        //        return Problem("Entity set 'DiabetesOnContainersContext.Assistants'  is null.");
        //    }

        //    var assistant = _mapper.Map<Assistant>(newOne);
        //    await _context.Assistants.AddAsync(assistant);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AssistantExists(assistant.Cin))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction(nameof(GetAssistant), new { cin = newOne.Cin }, assistant);
        //}

        //patch api/assistant/as1234

        //todo: assistant
        [HttpPost("Register")]
        public async Task<ActionResult> Register(AssistRegister register)
        {
            CreateHash(register.password, out byte[] passHash, out byte[] passSalt);

            var assist = _mapper.Map<Assistant>(register);
            assist.PasswordHash = passHash;
            assist.PasswordSalt = passSalt;

            await _context.Assistants.AddAsync(assist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssistantExists(assist.Cin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetAssistant), new { cin = assist.Cin }, assist);

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AssistLogin login)
        {
            var assist = await _context.Assistants.FirstOrDefaultAsync(q => q.Email == login.Email);
            if ( assist is null)
            {
                return NotFound("email inserted does not exists");
            }
            if (!Verfypassword(login.Password, assist.PasswordHash, assist.PasswordSalt))
            {
                return BadRequest("the password is not valid");
            }


            string Token = CreateToken(assist);
            return Ok(Token);
        }

        private string CreateToken(Assistant assist)
        {
            List<Claim> claims = new List<Claim>()
            { 
                new Claim(ClaimTypes.Name,assist.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials: cred
                );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPatch("{Cin}")]
        public async Task<IActionResult> PatchAssistant(string Cin, [FromBody] JsonPatchDocument<AssistCD> update)
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

            return AcceptedAtAction(nameof(GetAssistant), new { cin = Cin }, Assistant);
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
                      .FirstOrDefaultAsync(req => req.Cin == cin);

            return row;
        }
        private void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool Verfypassword(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var computedHash  = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passHash);
            }
        }


    }
}
