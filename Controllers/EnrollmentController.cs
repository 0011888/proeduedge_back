using Microsoft.AspNetCore.Mvc;
using proeduedge.DAL.Entities;
using proeduedge.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentsRepository _enrollmentRepo;

        public EnrollmentController(IEnrollmentsRepository enrollmentRepo)
        {
            _enrollmentRepo = enrollmentRepo;
        }
        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll(Enrollments enrollments)
        {
            var result = await _enrollmentRepo.Enroll(enrollments);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Course not found");
            }
        }

        [HttpGet("get-enrolled-students-by-courseId/{courseId}")]
        public async Task<IActionResult> GetEnrolledStudentsByCourseId(int courseId)
        {
            var students = await _enrollmentRepo.GetEnrolledStudentsByCourseId(courseId);
            return Ok(students);
        }

        [HttpGet("get-enrolled-students-by-instructorId/{userId}")]
        public async Task<IActionResult> GetEnrolledStudentsByInstructorId(int userId)
        {
            var students = await _enrollmentRepo.GetEnrolledStudentsByInstructorId(userId);
            return Ok(students);
        }

    }
}
