using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proeduedge.Models
{
    public class CourseFiles
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
