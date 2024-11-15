using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:protnumber/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GEt: // https://localhost:protnumber/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[]
            {
                "John", "Jane", "Mark", "Emaily", "David"
            };

            return Ok(studentNames);
        }
    }
}
