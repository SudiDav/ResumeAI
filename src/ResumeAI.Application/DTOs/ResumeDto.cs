namespace ResumeAI.Application.DTOs;

public class ResumeDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<ExperienceDto> WorkExperiences { get; set; } = new();
    public List<EducationDto> EducationHistory { get; set; } = new();
    public List<string> Skills { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}