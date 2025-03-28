using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeAI.Domain.Entities;
using ResumeAI.Infrastructure.Identity;

namespace ResumeAI.Infrastructure.Data;

// Using primary constructor with IdentityDbContext
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<CoverLetter> CoverLetters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the IdentityDbContext's OnModelCreating first to set up Identity tables
        base.OnModelCreating(modelBuilder);

        // Customize ASP.NET Identity model tables (optional)
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        // Link ApplicationUser to UserProfile
        modelBuilder.Entity<ApplicationUser>()
            .HasOne<UserProfile>()
            .WithOne()
            .HasForeignKey<UserProfile>(up => up.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Resume configuration
        modelBuilder.Entity<Resume>()
            .HasOne(r => r.User)
            .WithMany(u => u.Resumes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Resume>()
            .Property(r => r.Skills)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        // Experience configuration
        modelBuilder.Entity<Experience>()
            .HasOne(e => e.Resume)
            .WithMany(r => r.WorkExperiences)
            .HasForeignKey(e => e.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Education configuration
        modelBuilder.Entity<Education>()
            .HasOne(e => e.Resume)
            .WithMany(r => r.EducationHistory)
            .HasForeignKey(e => e.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // JobApplication configuration
        modelBuilder.Entity<JobApplication>()
            .HasOne(j => j.User)
            .WithMany(u => u.JobApplications)
            .HasForeignKey(j => j.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<JobApplication>()
            .HasOne(j => j.Resume)
            .WithMany()
            .HasForeignKey(j => j.ResumeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<JobApplication>()
            .HasOne(j => j.CoverLetter)
            .WithMany(c => c.JobApplications)
            .HasForeignKey(j => j.CoverLetterId)
            .OnDelete(DeleteBehavior.SetNull);

        // CoverLetter configuration
        modelBuilder.Entity<CoverLetter>()
            .HasOne(c => c.User)
            .WithMany(u => u.CoverLetters)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CoverLetter>()
            .HasOne(c => c.Resume)
            .WithMany()
            .HasForeignKey(c => c.ResumeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}