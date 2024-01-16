using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JoinIt_Backend.Shared.Data
{
    public class DatabaseContext : DbContext
    {
        public  virtual DbSet<Activity> Activities { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public DbSet<Zip> Zips { get; set; }

        public DbSet<Country> Countries { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Attendance> Attendances { get; set; }


        /// <summary>
        /// Handles construction of the context in design time.
        /// </summary>
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("data source=DESKTOP-G7M4EEG\\SQLEXPRESS;initial catalog=JoinIt-dev-v2;trusted_connection=true;Encrypt=True;TrustServerCertificate=True");
            }
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Attendances)
        //        .WithOne(a => a.User)
        //        .HasForeignKey(a => a.UserId);

        //    modelBuilder.Entity<Activity>()
        //       .HasMany(a => a.Attendants)
        //       .WithOne(a => a.Activity)
        //       .HasForeignKey(a => a.ActivityId);

        //    modelBuilder.Entity<Attendance>()
        //        .HasKey(a => new { a.UserId, a.ActivityId });

        //    modelBuilder.Entity<Attendance>()
        //        .HasOne(a => a.User)
        //        .WithMany(u => u.Attendances)
        //        .HasForeignKey(a => a.UserId);

        //    modelBuilder.Entity<Attendance>()
        //        .HasOne(a => a.Activity)
        //        .WithMany(a => a.Attendants)
        //        .HasForeignKey(a => a.ActivityId);


        //}
    }
}
