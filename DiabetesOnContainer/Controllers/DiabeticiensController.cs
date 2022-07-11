using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiabetesOnContainer.Models;
using DiabetesOnContainer.DTOs.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using DiabetesOnContainer.DTOs.Admin.log_In_Out;
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
    public class DiabeticiensController : ControllerBase
    {
        private readonly DiabetesOnContainersContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public DiabeticiensController(DiabetesOnContainersContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            this._mapper = mapper;
            this._configuration = configuration;
        }



        // GET: api/Diabeticiens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiabeticienREAD>>> GetDiabeticiens()
        {

            if (_context.Diabeticiens == null)
            {
                return NotFound();
            }
            //return Ok(_mapper.Map<IEnumerable<DiabeticienREAD>>
            //                (await _context.Diabeticiens.ToListAsync())
            //         );

            return Ok(await _context.Diabeticiens
                                            .ProjectTo<DiabeticienREAD>(_mapper.ConfigurationProvider)
                                            .ToListAsync());
        }



        // GET: api/Diabeticiens/med1234
        [HttpGet("{cin}")]

        public async Task<ActionResult<DiabeticienREAD>> GetDiabeticien(string cin)
        {
            if (_context.Diabeticiens == null)
            {
                return NotFound();
            }

            //var diabeticien = await _context.Diabeticiens.FindAsync(id);
            var diabeticien = await _context.Diabeticiens
                                    .ProjectTo<DiabeticienREAD>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync(q => q.Cin == cin);
            if (diabeticien == null)
            {
                return NotFound();
            }

            return Ok(diabeticien);
        }




        // PUT: api/Diabeticiens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cin}")]
        public async Task<IActionResult> PutDiabeticien(string cin, DiabeticienUpdate diabeticienDRUD)
        {
            if (!DiabeticienExists(cin))
            {
                return NotFound();
            }
            var casRow = await _context.Diabeticiens.FindAsync(cin);

            _mapper.Map(diabeticienDRUD, casRow);

            _context.Entry(casRow).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return NoContent();
        }



        // POST: api/Diabeticiens
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(DocRegister register)
        {
            CreateHash(register.password, out byte[] passHash, out byte[] passSalt);

            var doc = _mapper.Map<Diabeticien>(register);
            doc.PasswordHash = passHash;
            doc.PasswordSalt = passSalt;

            await _context.Diabeticiens.AddAsync(doc);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!DiabeticienExists(doc.Cin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetDiabeticien), new { cin = doc.Cin }, _mapper.Map<DiabeticienREAD>(doc));

        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(Doclogin login)
        {
            var doc = await _context.Diabeticiens.FirstOrDefaultAsync(q => q.Email == login.Email);
            if (doc is null)
            {
                return NotFound("email inserted does not exists");
            }
            if (!Verfypassword(login.Password, doc.PasswordHash, doc.PasswordSalt))
            {
                return BadRequest("the password is not valid");
            }


            string Token = CreateToken(doc.Email);
            return Ok(Token);
        }




        //patch api/assistant/as1234
        [HttpPatch("{Cin}")]
        public async Task<IActionResult> PatchDiabeticien(string Cin, [FromBody] JsonPatchDocument<DiabeticienCD> update)
        {
            var doc = AssistExistsPatch(Cin).Result;
            if (doc == null)
            {
                return NotFound("the cin does not exists in the table");
            }
            update.ApplyTo(doc);
            var value = _mapper.Map<Diabeticien>(doc);

            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return AcceptedAtAction(nameof(GetDiabeticien), new { cin = Cin }, doc);
        }


        // DELETE: api/Diabeticiens/5
        [HttpDelete("{cin}")]
        public async Task<IActionResult> DeleteDiabeticien(string cin)
        {
           
            var doc = await _context.Diabeticiens
                .Include(fk=>fk.FichePatients)
                .Include(fk=>fk.FicheMedicals)
                .FirstOrDefaultAsync(q=>q.Cin == cin);




            if (doc == null|| _context.Diabeticiens == null)
            {
                return NotFound();
            }

            foreach (var fichepat in doc.FichePatients)
            {
                fichepat.RefMed = null;
            }



            foreach (var med in doc.FicheMedicals)
            {
                med.RefMed = null;
            }

            _context.Diabeticiens.Remove(doc);
            await _context.SaveChangesAsync();

            return NoContent();
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
                new Claim(ClaimTypes.Role,"Doc")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
                );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool Verfypassword(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passHash);
            }
        }

        private bool DiabeticienExists(string cin)
        {
            return (_context.Diabeticiens?.Any(e => e.Cin == cin)).GetValueOrDefault();
        }
        private async Task<DiabeticienCD> AssistExistsPatch(string cin)
        {
            var row = await _context.Diabeticiens
                        .ProjectTo<DiabeticienCD>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync(req => req.Cin == cin);

            return row;
        }

    }
}
