using System.ComponentModel.DataAnnotations;

namespace ResumeAI.Application.DTOs;

public class RefreshTokenDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
}