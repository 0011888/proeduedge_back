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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseContent>()
                .HasOne(cc => cc.Owner)
                .WithMany()
                .HasForeignKey(cc => cc.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
