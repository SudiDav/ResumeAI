using ResumeAI.Domain.Entities;

namespace ResumeAI.Domain.Interfaces;

public interface IJobApplicationRepository : IRepository<JobApplication>
{
    Task<IEnumerable<JobApplication>> GetApplicationsByUserIdAsync(int userId);
    Task<IEnumerable<JobApplication>> GetApplicationsByStatusAsync(int userId, string status);
}