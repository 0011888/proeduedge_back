using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using proeduedge.DAL;
using proeduedge.Models.DTO;

namespace proeduedge.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDBContext _context;

        public StudentRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CourseClient>> GetCoursesByStudentId(int studentId)
        {
            var courses = await _context.Enrollments
                .Where(e => e.UserId == studentId)
                .Join(_context.Course,
                    enrollment => enrollment.CourseId,
                    course => course.Id,
                    (enrollment, course) => new { course, enrollment })
                .Join(_context.Users,
                    courseAndEnrollment => courseAndEnrollment.course.InstructorId,
                    instructor => instructor.Id,
                    (courseAndEnrollment, instructor) => new CourseClient
                    {
                        Id = courseAndEnrollment.course.Id,
                        Title = courseAndEnrollment.course.Title,
                        Description = courseAndEnrollment.course.Description,
                        Banner = courseAndEnrollment.course.Banner,
                        Price = courseAndEnrollment.course.Price,
                        InstructorId = courseAndEnrollment.course.InstructorId,
                        Instructor = instructor,
                        CategoryId = courseAndEnrollment.course.CategoryId,
                        CreatedAt = courseAndEnrollment.course.CreatedAt,
                        IsVerified = courseAndEnrollment.course.IsVerified
                    })
                .ToListAsync();

            return courses;
        }

        public async Task<bool> SetSectionStatus(int studentId, int sectionId, string status)
        {
            var courseContent = _context.StudentProgress.FirstOrDefault(sp => sp.UserId == studentId).Content;
            var courseContentList = JsonConvert.DeserializeObject<List<CourseSection>>(courseContent);
            var section = courseContentList.FirstOrDefault(s => s.Id == sectionId);
            if (section != null)
            {
                section.Status = status;
                var newCourseContent = JsonConvert.SerializeObject(courseContentList);
                _context.StudentProgress.FirstOrDefault(sp => sp.UserId == studentId).Content = newCourseContent;
                _context.SaveChanges();
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
