using Microsoft.EntityFrameworkCore;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Interfaces;
using ResumeAI.Infrastructure.Data;

namespace ResumeAI.Infrastructure.Repositories;

public class CoverLetterRepository(ApplicationDbContext context)
    : Repository<CoverLetter>(context), ICoverLetterRepository
{
    public async Task<IEnumerable<CoverLetter>> GetCoverLettersByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .Include(c => c.Resume)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<CoverLetter>> GetCoverLettersByResumeIdAsync(int resumeId)
    {
        return await _dbSet
            .Where(c => c.ResumeId == resumeId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}