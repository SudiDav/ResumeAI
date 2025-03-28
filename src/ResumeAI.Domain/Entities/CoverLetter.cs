namespace ResumeAI.Domain.Entities;

public class CoverLetter
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ResumeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
        
    // Navigation properties
    public UserProfile? User { get; set; }
    public Resume? Resume { get; set; }
    public List<JobApplication> JobApplications { get; set; } = new();
}