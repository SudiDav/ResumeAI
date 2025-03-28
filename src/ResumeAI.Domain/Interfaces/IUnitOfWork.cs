namespace ResumeAI.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IResumeRepository Resumes { get; }
    IUserProfileRepository UserProfiles { get; }
    IJobApplicationRepository JobApplications { get; }
    ICoverLetterRepository CoverLetters { get; }
        
    Task<int> SaveChangesAsync();
}