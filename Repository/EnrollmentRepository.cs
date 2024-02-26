using Microsoft.EntityFrameworkCore;
using proeduedge.DAL;
using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;

namespace proeduedge.Repository
{
    public class EnrollmentRepository : IEnrollmentsRepository
    {
        private readonly AppDBContext _context;

        public EnrollmentRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Enrollments> Enroll(Enrollments enrollments)
        {
            var course = await _context.CourseContent
                .Where(c => c.CourseId == enrollments.CourseId)
                .FirstOrDefaultAsync();
            if (course != null)
            {
                var content = course.Content;
                _context.StudentProgress.Add(new StudentsProgress
                {
                    CourseId = enrollments.CourseId,
                    UserId = enrollments.UserId,
                    Content = content,
                    ModifiedDate = DateTime.UtcNow
                });
                _context.Enrollments.Add(enrollments);
                await _context.SaveChangesAsync();
                return enrollments;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserClient>> GetEnrolledStudentsByCourseId(int courseId)
        {
            try
            {
                var enrollments = await _context.Enrollments
                    .Where(e => e.CourseId == courseId && e.Status == "Enrolled")
                    .Select(e => e.UserId)
                    .ToListAsync();

                var students = await _context.Users
                    .Where(u => enrollments.Contains(u.Id) && u.Role == "Student")
                    .Select(u => new UserClient
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AvatarUrl = u.AvatarUrl,
                        Email = u.Email,
                        Role = u.Role
                    })
                    .ToListAsync();

                return students;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while fetching enrolled students by course ID", ex);
            }
        }

        public async Task<IEnumerable<UserClient>> GetEnrolledStudentsByInstructorId(int userId)
        {
            try
            {
                var coursesTaughtByInstructor = await _context.Course
                    .Where(c => c.InstructorId == userId)
                    .Select(c => c.Id)
                    .ToListAsync();

                var enrollments = await _context.Enrollments
                    .Where(e => coursesTaughtByInstructor.Contains(e.CourseId) && e.Status == "Enrolled")
                    .Select(e => e.UserId)
                    .ToListAsync();

                var students = await _context.Users
                    .Where(u => enrollments.Contains(u.Id) && u.Role == "Student")
                    .Select(u => new UserClient
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AvatarUrl = u.AvatarUrl,
                        Email = u.Email,
                        Role = u.Role
                    })
                    .ToListAsync();

                return students;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while fetching enrolled students by instructor ID", ex);
            }
        }
    }
}
