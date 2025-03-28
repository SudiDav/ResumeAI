using Microsoft.EntityFrameworkCore;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Interfaces;
using ResumeAI.Infrastructure.Data;

namespace ResumeAI.Infrastructure.Repositories;

public class ResumeRepository(ApplicationDbContext context) : Repository<Resume>(context), IResumeRepository
{
    public async Task<IEnumerable<Resume>> GetResumesByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<Resume?> GetResumeWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(r => r.WorkExperiences)
            .Include(r => r.EducationHistory)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
