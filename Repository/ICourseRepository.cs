using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;

namespace proeduedge.Repository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<Course> GetCourse(int id);
        Task<Course> AddCourse(Course course);
        Task<Course> UpdateCourse(Course course);
        Task<string> DeleteCourse(int id);
        Task<CourseContentClientModel> AddCourseContent(CourseContentClientModel courseContent);
    }
}
