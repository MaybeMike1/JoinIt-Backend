using JoinIt_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Zip> Zips { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Attendants)
                .WithMany(u => u.ActiveActivities);
        }
    }
}
