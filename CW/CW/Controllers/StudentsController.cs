using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CW.DAL;
using CW.Models;
using CW.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CW.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        /*private readonly IDbService<Student> _dbService;
        public StudentsController(IDbService<Student> dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _dbService.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(string id)
        {
            var res = _dbService.Get(id);
            if(res == null)
            {
                return NotFound("Student not found");
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            if(_dbService.Add(student)) 
                return Ok(student);
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateStudent(Student student)
        {
            if(_dbService.Update(student))
                return Ok("Aktualizacja dokonczona");
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteStudent(Student student)
        {
            if(_dbService.Delete(student.IndexNumber))
                return Ok("Usuwanie ukonczone");
            return BadRequest();
        }*/

        public class AccountController : Controller
        {
            // danne testowe
            private List<Student> students = new List<Student>
        {
            new Student { Index="s19999", Password="qwerty", Role = "stud" },
            new Student { Index="s12222", Password="asdfgh", Role = "stud" }
        };

            [HttpPost("/token")]
            public IActionResult Token(string username, string password)
            {
                var identity = GetIdentity(username, password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }

                var now = DateTime.UtcNow;
                // JWT
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                return Json(response);
            }

            private ClaimsIdentity GetIdentity(string username, string password)
            {
                Student student = students.FirstOrDefault(x => x.Index == username && x.Password == password);
                if (student != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, student.Index),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, student.Role)
                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }

               
                return null;
            }
        }

    }
}