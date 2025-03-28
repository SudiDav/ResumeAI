using System.Security.Claims;

namespace ResumeAI.Infrastructure.Identity.Services;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
    Task<string> GenerateTokenAsync(ApplicationUser user, IEnumerable<Claim> additionalClaims);
    ClaimsPrincipal? GetPrincipalFromToken(string token);
}