using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize]
        [Route("getIndex")]
        public IActionResult GetIndex()
        {
            return Ok($"Indeks: {User.Identity.Name}");
        }

        [Authorize(Roles = "stud")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok();
        }
    }
}