using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proeduedge.DAL.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public int Price { get; set; }
        public int InstructorId { get; set; }
        [ForeignKey(nameof(Id))]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(Id))]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
    }
}
