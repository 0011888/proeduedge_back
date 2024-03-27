using proeduedge.Models.DTO;

namespace proeduedge.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<CourseClient>> GetCoursesByStudentId(int studentId);
        Task<bool> SetSectionStatus(int studentId, int sectionId, string status);
    }
}
