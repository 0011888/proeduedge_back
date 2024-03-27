﻿using proeduedge.DAL.Entities;

namespace proeduedge.Models.DTO
{
    public class CourseClient
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public int Price { get; set; }
        public int InstructorId { get; set; }
        public Users Instructor { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
    }
}
