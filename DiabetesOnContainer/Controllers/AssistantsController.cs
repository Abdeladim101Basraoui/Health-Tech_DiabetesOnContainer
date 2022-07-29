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
using Microsoft.AspNetCore.Authorization;

namespace DiabetesOnContainer.Controllers
{
    [Route("api/admin/[controller]")]

    [ApiController]
    [Authorize(Roles = "Doc")]
    public class AssistantsController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AssistantsController(DiabetesOnContainersContext context, IMapper mapper, IConfiguration configuration)
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
        [AllowAnonymous]
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
                if (!AssistantExists(assist.Cin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetAssistant), new { cin = assist.Cin }, _mapper.Map<AssistREAD>(assist));

        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(AssistLogin login)
        {
            var assist = await _context.Assistants.FirstOrDefaultAsync(q => q.Email == login.Email);
            if (assist is null)
            {
                return NotFound("email inserted does not exists");
            }
            if (!Verfypassword(login.Password, assist.PasswordHash, assist.PasswordSalt))
            {
                return BadRequest("the password is not valid");
            }


            string Token =  CreateToken(assist.Email);
            return Ok(Token);
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
            var assistant = await _context.Assistants
                .FirstOrDefaultAsync(req => req.Cin == cin);
            if (assistant == null || _context.Assistants == null)
            {
                return NotFound();
            }
        
            await _context.SaveChangesAsync();

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
        private string CreateToken(string email)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,email),
                new Claim(ClaimTypes.Role,"Assist")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            //Refresh Token
            var RefreshToken = GenerateRefreshToken(claims[1].Value);
            SetRefreshToken(RefreshToken);
            return jwt;
        }

        private RefreshToken GenerateRefreshToken(string Role)
        {
            var refreshtoken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddHours(2),
                Created = DateTime.Now,
                Role = Role
            };
            return refreshtoken;

        }
        
        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            Response.Cookies.Append("RefreshToken",refreshToken.Token,cookiesOptions);

            refreshToken.Id = Guid.NewGuid();
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        
        }



        private bool Verfypassword(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passHash);
            }
        }


    }
}
