using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proeduedge.DAL.Entities
{
    public class CourseContent
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(Id))]
        public Course Course { get; set; }

        public int OwnerId { get; set; }
        [ForeignKey(nameof(Id))]
        public Users Owner { get; set; }
        public string Content { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
