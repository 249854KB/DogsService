using DogsService.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<User> Users{ get; set; }
        public DbSet<Dog> Dogs{ get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<User>()
            .HasMany(u => u.Dogs)
            .WithOne(u=> u.User!)
            .HasForeignKey(u=>u.UserId);

            modelBuilder
            .Entity<Dog>()
            .HasOne(u=>u.User)
            .WithMany(u=>u.Dogs)
            .HasForeignKey(u=>u.UserId);
        }
    }
}
