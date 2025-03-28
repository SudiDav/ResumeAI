using Microsoft.EntityFrameworkCore;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Interfaces;
using ResumeAI.Infrastructure.Data;

namespace ResumeAI.Infrastructure.Repositories;

public class UserProfileRepository(ApplicationDbContext context)
    : Repository<UserProfile>(context), IUserProfileRepository
{
    public async Task<UserProfile?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserProfile?> GetProfileWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Resumes)
            .Include(u => u.JobApplications)
            .Include(u => u.CoverLetters)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}