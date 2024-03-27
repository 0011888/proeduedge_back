using proeduedge.DAL;
using proeduedge.DAL.Entities;
using proeduedge.Models.DTO;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CourseClient> GetCourse(int id)
        {
            var courseWithInstructor = await _context.Course
                .Join(_context.Users,
                course => course.InstructorId, // Key selector from the first sequence
                instructor => instructor.Id, // Key selector from the second sequence
                (course, instructor) => new CourseClient // Result selector
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Banner = course.Banner,
                    Price = course.Price,
                    InstructorId = course.InstructorId,
                    Instructor = new Users
                    {
                        Id = instructor.Id,
                        FirstName = instructor.FirstName,
                        LastName = instructor.LastName,
                        AvatarUrl = instructor.AvatarUrl,
                        Email = instructor.Email,
                        Role = instructor.Role
                    },
                    CategoryId = course.CategoryId,
                    CreatedAt = course.CreatedAt,
                    IsVerified = course.IsVerified
                }).FirstOrDefaultAsync(c => c.Id == id);
            if (courseWithInstructor != null)
            {
                return courseWithInstructor;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<CourseClient>> GetCourses()
        {
            var coursesWithInstructors = await _context.Course
                .Join(_context.Users, // The second sequence to join
                      course => course.InstructorId, // Key selector from the first sequence
                      instructor => instructor.Id, // Key selector from the second sequence
                      (course, instructor) => new CourseClient // Result selector
                      {
                          Id = course.Id,
                          Title = course.Title,
                          Description = course.Description,
                          Banner = course.Banner,
                          Price = course.Price,
                          InstructorId = course.InstructorId,
                          Instructor = new Users
                          {
                              Id = instructor.Id,
                              FirstName = instructor.FirstName,
                              LastName = instructor.LastName,
                              AvatarUrl = instructor.AvatarUrl,
                              Email = instructor.Email,
                              // Exclude Password for security reasons
                              Role = instructor.Role
                          },
                          CategoryId = course.CategoryId,
                          CreatedAt = course.CreatedAt,
                          IsVerified = course.IsVerified
                      })
                .ToListAsync();

            return coursesWithInstructors;
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
        public async Task<IEnumerable<CourseStat>> GetInstructorCourses(int id)
        {
            // Initialize a Random class instance
            var random = new Random();

            var courses = await _context.Course
                .Where(c => c.InstructorId == id)
                .Select(c => new
                {
                    Course = c,
                    TotalEarning = _context.Payment
                        .Where(p => p.CourseId == c.Id)
                        .Sum(p => p.Amount),
                    TotalStudents = _context.Enrollments
                        .Count(e => e.CourseId == c.Id && e.Status == "enrolled")
                })
                .ToListAsync();

            var courseStats = courses.Select(c => new CourseStat
            {
                Id = c.Course.Id,
                Title = c.Course.Title,
                Description = c.Course.Description,
                Banner = c.Course.Banner,
                Price = c.Course.Price,
                InstructorId = c.Course.InstructorId,
                CategoryId = c.Course.CategoryId,
                CreatedAt = c.Course.CreatedAt,
                IsVerified = c.Course.IsVerified,
                Rating = random.Next(1, 6), // Generate a random number between 1 and 5
                TotalEarning = c.TotalEarning,
                TotalStudents = c.TotalStudents
            }).ToList();

            return courseStats;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
