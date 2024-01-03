using Microsoft.EntityFrameworkCore;
using System;

namespace proeduedge.Models
{
    public class AppDBContext : DbContext
    {
        

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
          }
}
