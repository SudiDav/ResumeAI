namespace ResumeAI.Domain.Entities;

public class Resume
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<Experience> WorkExperiences { get; set; } = new();
    public List<Education> EducationHistory { get; set; } = new();
    public List<string> Skills { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation property
    public UserProfile? User { get; set; }
}