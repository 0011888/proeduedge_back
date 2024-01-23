using Microsoft.EntityFrameworkCore;
using proeduedge.DAL.Entities;

namespace proeduedge.DAL
{
    public class AppDBContext : DbContext
    {


        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseContent> CourseContent { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Payment> Payment { get; set; }
    }
}
