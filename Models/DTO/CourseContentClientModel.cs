namespace proeduedge.Models.DTO
{

    public class CourseContentClientModel
    {
        public int CourseId { get; set; }
        public int OwnerId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<CourseSection> CourseContent { get; set; }

        public CourseContentClientModel()
        {
            CourseContent = new List<CourseSection>();
        }
    }

    public class CourseSection
    {
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
}
