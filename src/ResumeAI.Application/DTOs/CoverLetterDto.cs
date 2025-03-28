namespace ResumeAI.Application.DTOs;

public class CoverLetterDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ResumeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}