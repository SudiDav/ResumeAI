using ResumeAI.Application.DTOs;
using ResumeAI.Domain.Interfaces;
using ResumeAI.Domain.Services;

namespace ResumeAI.Application.Services;

public class AIResumeService(IUnitOfWork unitOfWork, IAIService aiService) : IAIResumeService
{
    public async Task<string> GenerateImprovedResumeAsync(ResumeImprovementDto resumeImprovementDto)
    {
        var resume = await unitOfWork.Resumes.GetResumeWithDetailsAsync(resumeImprovementDto.ResumeId)
            ?? throw new KeyNotFoundException($"Resume with ID {resumeImprovementDto.ResumeId} not found");
        
        return await aiService.GenerateImprovedResumeAsync(resume, resumeImprovementDto.JobDescription);
    }

    public async Task<List<string>> SuggestSkillImprovementsAsync(SkillSuggestionDto skillSuggestionDto)
    {
        var resume = await unitOfWork.Resumes.GetResumeWithDetailsAsync(skillSuggestionDto.ResumeId)
            ?? throw new KeyNotFoundException($"Resume with ID {skillSuggestionDto.ResumeId} not found");
        
        return await aiService.SuggestSkillImprovementsAsync(resume, skillSuggestionDto.JobDescription);
    }

    public async Task<ResumeAnalysisDto> AnalyzeResumeAsync(int resumeId)
    {
        var resume = await unitOfWork.Resumes.GetResumeWithDetailsAsync(resumeId)
            ?? throw new KeyNotFoundException($"Resume with ID {resumeId} not found");
        
        var analysis = await aiService.AnalyzeResumeAsync(resume);
        
        // Parse the analysis (in a real application, you would parse the AI response more robustly)
        var analysisDto = new ResumeAnalysisDto
        {
            Analysis = analysis,
            Strengths = ExtractStrengths(analysis),
            Weaknesses = ExtractWeaknesses(analysis),
            Suggestions = ExtractSuggestions(analysis)
        };
        
        return analysisDto;
    }

    private List<string> ExtractStrengths(string analysis)
    {
        // This is a simplistic implementation
        // In a real application, you would parse the AI response more robustly
        return new List<string> { "Extracted strengths would go here" };
    }

    private List<string> ExtractWeaknesses(string analysis)
    {
        // This is a simplistic implementation
        // In a real application, you would parse the AI response more robustly
        return new List<string> { "Extracted weaknesses would go here" };
    }

    private List<string> ExtractSuggestions(string analysis)
    {
        // This is a simplistic implementation
        // In a real application, you would parse the AI response more robustly
        return new List<string> { "Extracted suggestions would go here" };
    }
}