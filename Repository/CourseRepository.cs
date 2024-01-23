using proeduedge.DAL;
using proeduedge.DAL.Entities;

namespace proeduedge.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDBContext _context;

        public CourseRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourse(Course course)
        {
            _context.Course.Add(course);
            await SaveAsync();
            return course;
        }

        public async Task<string> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
                await SaveAsync();
                return "Course deleted successfully";
            }
            else
            {
                return "Course not found";
            }
        }

        public async Task<Course> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if(course != null)
            {
                return course;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await Task.FromResult(_context.Course.ToList());
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            var courseToUpdate = await _context.Course.FindAsync(course.Id);
            if(courseToUpdate == null)
            {
                return null;
            }

            courseToUpdate.Title = course.Title;
            courseToUpdate.Description = course.Description;
            courseToUpdate.CategoryId = course.CategoryId;
            courseToUpdate.Banner = course.Banner;
            courseToUpdate.Price = course.Price; 
            courseToUpdate.IsVerified = course.IsVerified;
            await SaveAsync();
            return courseToUpdate;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
