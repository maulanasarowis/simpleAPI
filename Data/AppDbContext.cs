// Data/AppDbContext.cs

using Microsoft.EntityFrameworkCore;
using simpleAPI.Models;

namespace simpleAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Role).HasMaxLength(20);
        }
    }
}
