using ResumeAI.Domain.Interfaces;
using ResumeAI.Infrastructure.Data;

namespace ResumeAI.Infrastructure.Repositories;

public class UnitOfWork(
    ApplicationDbContext context,
    IResumeRepository resumeRepository,
    IUserProfileRepository userProfileRepository,
    IJobApplicationRepository jobApplicationRepository,
    ICoverLetterRepository coverLetterRepository)
    : IUnitOfWork
{
    private IResumeRepository _resumeRepository = resumeRepository;
    private IUserProfileRepository _userProfileRepository = userProfileRepository;
    private IJobApplicationRepository _jobApplicationRepository = jobApplicationRepository;
    private ICoverLetterRepository _coverLetterRepository = coverLetterRepository;
    private bool _disposed = false;

    public IResumeRepository Resumes => _resumeRepository ??= new ResumeRepository(context);

    public IUserProfileRepository UserProfiles => _userProfileRepository ??= new UserProfileRepository(context);

    public IJobApplicationRepository JobApplications => _jobApplicationRepository ??= new JobApplicationRepository(context);

    public ICoverLetterRepository CoverLetters => _coverLetterRepository ??= new CoverLetterRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}