using System.ComponentModel.DataAnnotations;

namespace proeduedge.DAL.Entities
{
    public class StudentsProgress
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
