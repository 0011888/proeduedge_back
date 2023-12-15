using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proeduedge.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Questions { get; set; }

        public bool IsVerified { get; set; }
    }
}
