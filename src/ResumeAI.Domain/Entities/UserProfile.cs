namespace ResumeAI.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string LinkedInProfile { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
        
    // Navigation properties
    public List<Resume> Resumes { get; set; } = new();
    public List<JobApplication> JobApplications { get; set; } = new();
    public List<CoverLetter> CoverLetters { get; set; } = new();
}