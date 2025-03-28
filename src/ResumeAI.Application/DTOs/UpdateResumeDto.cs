namespace ResumeAI.Application.DTOs;

public class UpdateResumeDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<ExperienceDto> WorkExperiences { get; set; } = new();
    public List<EducationDto> EducationHistory { get; set; } = new();
    public List<string> Skills { get; set; } = new();
}