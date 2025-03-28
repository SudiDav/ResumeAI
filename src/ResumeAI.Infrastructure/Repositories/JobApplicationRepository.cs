using Microsoft.EntityFrameworkCore;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Enums;
using ResumeAI.Domain.Interfaces;
using ResumeAI.Infrastructure.Data;

namespace ResumeAI.Infrastructure.Repositories;

public class JobApplicationRepository(ApplicationDbContext context)
    : Repository<JobApplication>(context), IJobApplicationRepository
{
    public async Task<IEnumerable<JobApplication>> GetApplicationsByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(j => j.UserId == userId)
            .Include(j => j.Resume)
            .Include(j => j.CoverLetter)
            .OrderByDescending(j => j.AppliedOn)
            .ToListAsync();
    }

    public async Task<IEnumerable<JobApplication>> GetApplicationsByStatusAsync(int userId, string status)
    {
        return await _dbSet
            .Where(j => j.UserId == userId && j.ApplicationStatus == ApplicationStatus.Pending)
            .Include(j => j.Resume)
            .Include(j => j.CoverLetter)
            .OrderByDescending(j => j.AppliedOn)
            .ToListAsync();
    }
}