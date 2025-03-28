using AutoMapper;
using ResumeAI.Application.DTOs;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Interfaces;

namespace ResumeAI.Application.Services;

public class UserProfileService(IUnitOfWork unitOfWork, IMapper mapper) : IUserProfileService
{
    public async Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync()
    {
        var userProfiles = await unitOfWork.UserProfiles.GetAllAsync();
        return mapper.Map<IEnumerable<UserProfileDto>>(userProfiles);
    }

    public async Task<UserProfileDto?> GetUserProfileByIdAsync(int id)
    {
        var userProfile = await unitOfWork.UserProfiles.GetProfileWithDetailsAsync(id);
        return userProfile != null ? mapper.Map<UserProfileDto>(userProfile) : null;
    }

    public async Task<UserProfileDto?> GetUserProfileByEmailAsync(string email)
    {
        var userProfile = await unitOfWork.UserProfiles.GetByEmailAsync(email);
        return userProfile != null ? mapper.Map<UserProfileDto>(userProfile) : null;
    }

    public async Task<UserProfileDto> CreateUserProfileAsync(CreateUserProfileDto createUserProfileDto)
    {
        var userProfile = mapper.Map<UserProfile>(createUserProfileDto);
        
        await unitOfWork.UserProfiles.AddAsync(userProfile);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<UserProfileDto>(userProfile);
    }

    public async Task<UserProfileDto> UpdateUserProfileAsync(int id, UpdateUserProfileDto updateUserProfileDto)
    {
        var userProfile = await unitOfWork.UserProfiles.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"User profile with ID {id} not found");
        
        mapper.Map(updateUserProfileDto, userProfile);
        
        await unitOfWork.UserProfiles.UpdateAsync(userProfile);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<UserProfileDto>(userProfile);
    }

    public async Task DeleteUserProfileAsync(int id)
    {
        await unitOfWork.UserProfiles.DeleteAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}