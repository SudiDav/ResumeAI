namespace ResumeAI.Domain.Entities;

public class Education
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
        
    // Navigation property
    public Resume? Resume { get; set; }
}