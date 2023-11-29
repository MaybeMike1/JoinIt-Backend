using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace JoinIt_Backend.Shared.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Zip> Zips { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(u => u.Attendances)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Activity>()
               .HasMany(a => a.Attendants)
               .WithOne(a => a.Activity)
               .HasForeignKey(a => a.ActivityId);

            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.UserId, a.ActivityId });

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.User)
                .WithMany(u => u.Attendances)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Activity)
                .WithMany(a => a.Attendants)
                .HasForeignKey(a => a.ActivityId);

                
        }
    }
}
