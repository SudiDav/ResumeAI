namespace ResumeAI.Application.DTOs;

public class EducationDto
{
    public int Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
}