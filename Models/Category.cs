using System.ComponentModel.DataAnnotations;

namespace proeduedge.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}
