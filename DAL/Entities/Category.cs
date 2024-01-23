using System.ComponentModel.DataAnnotations;

namespace proeduedge.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
