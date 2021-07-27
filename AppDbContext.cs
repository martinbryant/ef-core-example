using ef_core_example.Models;
using Microsoft.EntityFrameworkCore;

namespace ef_core_example
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("profile");
        }
    }
}