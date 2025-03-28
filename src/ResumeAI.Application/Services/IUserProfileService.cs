using ResumeAI.Application.DTOs;

namespace ResumeAI.Application.Services;

public interface IUserProfileService
{
    Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync();
    Task<UserProfileDto?> GetUserProfileByIdAsync(int id);
    Task<UserProfileDto?> GetUserProfileByEmailAsync(string email);
    Task<UserProfileDto> CreateUserProfileAsync(CreateUserProfileDto createUserProfileDto);
    Task<UserProfileDto> UpdateUserProfileAsync(int id, UpdateUserProfileDto updateUserProfileDto);
    Task DeleteUserProfileAsync(int id);
}