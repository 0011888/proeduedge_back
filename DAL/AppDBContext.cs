using Microsoft.EntityFrameworkCore;
using proeduedge.Models;
using System;

namespace proeduedge.DAL
{
    public class AppDBContext : DbContext
    {


        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
    }
}
