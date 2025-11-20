using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Models;
using System.Text.Json;

namespace ResumeMaker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            builder.Entity<IdentityRole>().HasData(user);//adding user role

            // Configure Resume to ApplicationUser relationship
            builder.Entity<Resume>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships
            builder.Entity<Experience>()
                .HasOne(e => e.Resume)
                .WithMany(r => r.Experiences)
                .HasForeignKey(e => e.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Education>()
                .HasOne(e => e.Resume)
                .WithMany(r => r.EducationHistory)
                .HasForeignKey(e => e.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Certification>()
                .HasOne(c => c.Resume)
                .WithMany(r => r.Certifications)
                .HasForeignKey(c => c.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Project>()
                .HasOne(p => p.Resume)
                .WithMany(r => r.Projects)
                .HasForeignKey(p => p.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Language>()
                .HasOne(l => l.Resume)
                .WithMany(r => r.Languages)
                .HasForeignKey(l => l.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Skills as JSON column
            builder.Entity<Resume>()
                .Property(r => r.Skills)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                );
        }
    }
}
