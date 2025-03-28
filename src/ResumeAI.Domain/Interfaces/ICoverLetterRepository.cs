using ResumeAI.Domain.Entities;

namespace ResumeAI.Domain.Interfaces;

public interface ICoverLetterRepository : IRepository<CoverLetter>
{
    Task<IEnumerable<CoverLetter>> GetCoverLettersByUserIdAsync(int userId);
    Task<IEnumerable<CoverLetter>> GetCoverLettersByResumeIdAsync(int resumeId);
}