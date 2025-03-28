namespace ResumeAI.Application.DTOs;

public class JobApplicationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ResumeId { get; set; }
    public int? CoverLetterId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public string ApplicationStatus { get; set; } = string.Empty;
    public DateTime AppliedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
}