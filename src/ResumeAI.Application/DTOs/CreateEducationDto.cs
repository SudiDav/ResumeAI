namespace ResumeAI.Application.DTOs;

public class CreateEducationDto
{
    public string Degree { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
}