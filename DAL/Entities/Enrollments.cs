using System.ComponentModel.DataAnnotations;

namespace proeduedge.DAL.Entities
{
    public class Enrollments
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }

}
