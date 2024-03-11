using System.ComponentModel.DataAnnotations;

namespace proeduedge.DAL.Entities
{
    public class Meetings
    {
        [Key]
        public int Id { get; set; }
        public string RoomId { get; set; }
        public bool IsActive { get; set; }
    }
}
