using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResumeAI.Application.DTOs;
using ResumeAI.Application.Services;
using ResumeAI.Infrastructure.Identity;

namespace ResumeAI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserProfileController(
    IUserProfileService userProfileService,
    UserManager<ApplicationUser> userManager)
    : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> GetCurrentUserProfile()
    {
        var userId = GetCurrentUserId();
        var userProfile = await userProfileService.GetUserProfileByIdAsync(userId);
        
        if (userProfile == null)
            return NotFound(new { Message = "Profile not found. Please create your profile." });
                
        return Ok(userProfile);
    }
    
    [HttpGet("identity")]
    public async Task<ActionResult> GetCurrentUserIdentity()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }
        
        return Ok(new
        {
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.PhoneNumber,
            user.LastLogin
        });
    }

    [HttpPost]
    public async Task<ActionResult<UserProfileDto>> CreateUserProfile(CreateUserProfileDto createUserProfileDto)
    {
        var userProfile = await userProfileService.CreateUserProfileAsync(createUserProfileDto);
        
        // Associate the user profile with the identity user
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return CreatedAtAction(nameof(GetCurrentUserProfile), userProfile);
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return CreatedAtAction(nameof(GetCurrentUserProfile), userProfile);
        user.UserProfileId = userProfile.Id;
        await userManager.UpdateAsync(user);

        return CreatedAtAction(nameof(GetCurrentUserProfile), userProfile);

    }

    [HttpPut("me")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var userProfile = await userProfileService.UpdateUserProfileAsync(userId, updateUserProfileDto);
            return Ok(userProfile);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = "Profile not found. Please create your profile first." });
        }
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // In a real application, you would need to map the Identity user ID (string)
        // to your domain user ID (int) by fetching the user profile from the database
        // This is a simplified example
        return int.TryParse(userIdClaim, out int userId) ? userId :
            // Default value (should be handled better in production)
            1;
    }
}