using Microsoft.AspNetCore.Mvc;
using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;
using proeduedge.Repository;
using static System.Collections.Specialized.BitVector32;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("all-courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseRepository.GetCourses();
            return Ok(courses);
        }

        [HttpGet("course/{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _courseRepository.GetCategories();
            return Ok(categories);
        }

        [HttpPost("create-course")]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            var newCourse = await _courseRepository.AddCourse(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = newCourse.Id }, newCourse);
        }
        [HttpPost("add-content")]
        public async Task<IActionResult> AddCourseContent([FromBody] CourseContentClientModel content)
        {
            try
            {
                await _courseRepository.AddCourseContent(content);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-content/{id}")]
        public async Task<IActionResult> GetCourseContent(int id)
        {
            var content = await _courseRepository.GetCourseWithContent(id);
            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpPut("edit-course")]
        public async Task<IActionResult> UpdateCourse([FromBody] Course course)
        {
            var existingCourse = await _courseRepository.GetCourse(course.Id);
            if (existingCourse == null)
            {
                return NotFound();
            }
            var updatedCourse = await _courseRepository.UpdateCourse(course);
            return Ok(updatedCourse);
        }

        [HttpDelete("delete-course/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            await _courseRepository.DeleteCourse(id);
            return NoContent();
        }

    }
}
