namespace ResumeAI.Application.DTOs;

public class CreateCoverLetterDto
{
    public int ResumeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}