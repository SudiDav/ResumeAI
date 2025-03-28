using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ResumeAI.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<string>
{
    [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(200)]
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
        
    public int? UserProfileId { get; set; }
}