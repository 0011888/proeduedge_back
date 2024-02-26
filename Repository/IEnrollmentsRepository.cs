using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;

namespace proeduedge.Repository
{
    public interface IEnrollmentsRepository
    {
        Task<Enrollments> Enroll(Enrollments enrollments);
        Task<IEnumerable<UserClient>> GetEnrolledStudentsByCourseId(int courseId);
        Task<IEnumerable<UserClient>> GetEnrolledStudentsByInstructorId(int userId);
    }
}
