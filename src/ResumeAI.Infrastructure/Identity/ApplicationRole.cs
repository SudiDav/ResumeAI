using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ResumeAI.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<string>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
}