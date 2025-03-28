using ResumeAI.Domain.Enums;

namespace ResumeAI.Domain.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ResumeId { get; set; }
    public int? CoverLetterId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public ApplicationStatus ApplicationStatus { get; set; } 
    public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdated { get; set; }
        
    // Navigation properties
    public UserProfile? User { get; set; }
    public Resume? Resume { get; set; }
    public CoverLetter? CoverLetter { get; set; }
}