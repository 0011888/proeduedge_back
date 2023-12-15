using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proeduedge.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsVerified { get; set; }
    }
}
