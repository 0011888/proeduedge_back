using Microsoft.AspNetCore.Mvc;
using proeduedge.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet("student-courses/{id}")]
        public async Task<IActionResult> GetCoursesByStudentId(int id)
        {
            var courses = await _studentRepository.GetCoursesByStudentId(id);
            return Ok(courses);
        }
    }
}
