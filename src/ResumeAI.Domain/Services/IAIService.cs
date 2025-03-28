using ResumeAI.Domain.Entities;

namespace ResumeAI.Domain.Services;

public interface IAIService
{
    Task<string> GenerateImprovedResumeAsync(Resume resume, string jobDescription);
    Task<string> GenerateCoverLetterAsync(Resume resume, string jobDescription);
    Task<List<string>> SuggestSkillImprovementsAsync(Resume resume, string jobDescription);
    Task<string> AnalyzeResumeAsync(Resume resume);
}