using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;

namespace proeduedge.Repository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseClient>> GetCourses();
        Task<CourseClient> GetCourse(int id);
        Task<Course> AddCourse(Course course); 
        Task<CourseSection> AddSection(CourseSection section);
        Task<Course> UpdateCourse(Course course);
        Task<string> DeleteCourse(int id);
        Task AddCourseContent(CourseContentClientModel courseContent);
        Task<IEnumerable<CourseStat>> GetInstructorCourses(int id);
        Task<CourseDetailDto> GetCourseWithContent(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
