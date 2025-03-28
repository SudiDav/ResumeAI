namespace ResumeAI.Application.DTOs;

public class CreateUserProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string LinkedInProfile { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
}