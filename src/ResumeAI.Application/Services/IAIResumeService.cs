using ResumeAI.Application.DTOs;

namespace ResumeAI.Application.Services;

public interface IAIResumeService
{
    Task<string> GenerateImprovedResumeAsync(ResumeImprovementDto resumeImprovementDto);
    Task<List<string>> SuggestSkillImprovementsAsync(SkillSuggestionDto skillSuggestionDto);
    Task<ResumeAnalysisDto> AnalyzeResumeAsync(int resumeId);
}