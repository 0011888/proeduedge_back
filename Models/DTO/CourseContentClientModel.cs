namespace proeduedge.Models.DTO
{

    public class CourseContentClientModel
    {
        public int CourseId { get; set; }
        public int OwnerId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public CourseSection CourseContent { get; set; }

    }

    public class CourseSection
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string SectionName { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<Resource> Resources { get; set; }

        public CourseSection()
        {
            Resources = new List<Resource>();
        }
    }

    public class Resource
    {
        public string Id { get; set; }
        public string FileType { get; set; }
        public string Url { get; set; }
    }

    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public int Price { get; set; }
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }
        public List<CourseSection> Contents { get; set; }
    }

    public class CourseContentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int OwnerId { get; set; }
        public string Content { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
