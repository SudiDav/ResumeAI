using ResumeAI.Domain.Entities;

namespace ResumeAI.Domain.Interfaces;

public interface IUserProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> GetByEmailAsync(string email);
    Task<UserProfile?> GetProfileWithDetailsAsync(int id);
}