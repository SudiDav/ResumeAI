using ResumeAI.Domain.Entities;

namespace ResumeAI.Domain.Interfaces;

public interface IResumeRepository : IRepository<Resume>
{
    Task<IEnumerable<Resume>> GetResumesByUserIdAsync(int userId);
    Task<Resume?> GetResumeWithDetailsAsync(int id);
}