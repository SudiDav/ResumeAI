namespace ResumeAI.Application.DTOs;

public class GenerateCoverLetterDto
{
    public int ResumeId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
}