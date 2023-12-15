using System.ComponentModel.DataAnnotations.Schema;

namespace proeduedge.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users User { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; }
    }
}
