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
<<<<<<< HEAD
        Task AddCourseContent(CourseContentClientModel courseContent);
        Task<CourseDetailDto> GetCourseWithContent(int id);
        Task<IEnumerable<Category>> GetCategories();
=======
        Task<CourseContentClientModel> AddCourseContent(CourseContentClientModel courseContent);
>>>>>>> 990db1e4cdf707b8b51d502627517e4e94d9fab0
    }
}
