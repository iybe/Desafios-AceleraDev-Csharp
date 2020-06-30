using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Models
{
    public class CodenationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Acceleration> Accelerations { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Codenation;Trusted_Connection=True");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>().HasKey(c => new { c.User_Id, c.Acceleration_Id, c.Company_Id, c.Status, c.Created_At });
            modelBuilder.Entity<Submission>().HasKey(s => new { s.User_Id, s.Challenge_Id, s.Score, s.Created_At });

            modelBuilder.Entity<Submission>()
                .HasOne(u => u.User)
                .WithMany(s => s.Submissions)
                .HasForeignKey(u => u.User_Id);

            modelBuilder.Entity<Submission>()
                .HasOne(c => c.Challenge)
                .WithMany(s => s.Submissions)
                .HasForeignKey(c => c.Challenge_Id);

            modelBuilder.Entity<Acceleration>()
                .HasOne(a => a.Challenge)
                .WithMany(c => c.Accelerations)
                .HasForeignKey(c => c.Challenge_Id);

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Acceleration)
                .WithMany(a => a.Candidates)
                .HasForeignKey(a => a.Acceleration_Id);

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.User)
                .WithMany(u => u.Candidates)
                .HasForeignKey(u => u.User_Id);

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Candidates)
                .HasForeignKey(c => c.Company_Id);
        }
    }
}