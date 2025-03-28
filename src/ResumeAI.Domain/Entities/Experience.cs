namespace ResumeAI.Domain.Entities;

public class Experience
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Responsibilities { get; set; } = string.Empty;
        
    // Navigation property
    public Resume? Resume { get; set; }
}