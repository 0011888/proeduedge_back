using proeduedge.DAL;
using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

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
        public async Task AddCourseContent(CourseContentClientModel courseContent)
        {
            var courseId = courseContent.CourseId;
            var ownerId = courseContent.OwnerId;
            var section = courseContent.CourseContent;
            var content = await _context.CourseContent.FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (content == null)
            {
                content = new CourseContent
                {
                    CourseId = courseId,
                    OwnerId = ownerId,
                    ModifiedDate = DateTime.UtcNow,
                    Content = JsonConvert.SerializeObject(new List<CourseSection> { section })
                };
                _context.CourseContent.Add(content);
            }
            else
            {
                var sections = JsonConvert.DeserializeObject<List<CourseSection>>(content.Content);
                sections.Add(section);
                content.Content = JsonConvert.SerializeObject(sections);
                content.ModifiedDate = DateTime.UtcNow;
                _context.CourseContent.Update(content);
            }

            await _context.SaveChangesAsync();
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

        public async Task<CourseDetailDto> GetCourseWithContent(int id)
        {
            // First, get the course with its related contents from the database
            var course = await _context.Course
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            var content = await _context.CourseContent
                .Where(c => c.CourseId == id)
                .FirstOrDefaultAsync();

            if (course == null || content == null)
            {
                return null;
            }

            // Then, perform the JSON deserialization in-memory
            var courseDetailDto = new CourseDetailDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Banner = course.Banner,
                Price = course.Price,
                InstructorId = course.InstructorId,
                CategoryId = course.CategoryId,
                CreatedAt = course.CreatedAt,
                IsVerified = course.IsVerified,
                Contents = JsonConvert.DeserializeObject<List<CourseSection>>(content.Content)
            };

            return courseDetailDto;
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

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await Task.FromResult(_context.Category.ToList());
        }   
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
