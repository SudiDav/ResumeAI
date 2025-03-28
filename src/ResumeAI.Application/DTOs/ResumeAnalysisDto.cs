namespace ResumeAI.Application.DTOs;

public class ResumeAnalysisDto
{
    public string Analysis { get; set; } = string.Empty;
    public List<string> Strengths { get; set; } = new();
    public List<string> Weaknesses { get; set; } = new();
    public List<string> Suggestions { get; set; } = new();
}