using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeAI.Application.DTOs;
using ResumeAI.Application.Services;

namespace ResumeAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class AIController(IAIResumeService aiResumeService) : ControllerBase
{
    [HttpPost("improve-resume")]
    public async Task<ActionResult<string>> ImproveResume(ResumeImprovementDto resumeImprovementDto)
    {
        var improvedResume = await aiResumeService.GenerateImprovedResumeAsync(resumeImprovementDto);
        return Ok(improvedResume);
    }

    [HttpPost("suggest-skills")]
    public async Task<ActionResult<List<string>>> SuggestSkills(SkillSuggestionDto skillSuggestionDto)
    {
        var suggestedSkills = await aiResumeService.SuggestSkillImprovementsAsync(skillSuggestionDto);
        return Ok(suggestedSkills);
    }

    [HttpGet("analyze-resume/{resumeId}")]
    public async Task<ActionResult<ResumeAnalysisDto>> AnalyzeResume(int resumeId)
    {
        try
        {
            var analysis = await aiResumeService.AnalyzeResumeAsync(resumeId);
            return Ok(analysis);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}