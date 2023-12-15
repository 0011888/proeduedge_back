using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proeduedge.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public Users Instructor { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public string Duration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsVerified { get; set; }

        public List<Enrollment> Enrollments { get; set; }

        public List<CourseFiles> CourseFiles { get; set; }

        public List<Assignment> Assignments { get; set; }

        public List<Quiz> Quizzes { get; set; }
    }
}
