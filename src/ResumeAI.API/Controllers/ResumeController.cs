using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeAI.Application.DTOs;
using ResumeAI.Application.Services;

namespace ResumeAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class ResumeController(IResumeService resumeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResumeDto>>> GetAllResumes()
    {
        var userId = GetCurrentUserId();
        var resumes = await resumeService.GetAllResumesAsync(userId);
        return Ok(resumes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResumeDto>> GetResumeById(int id)
    {
        var resume = await resumeService.GetResumeByIdAsync(id);
        
        if (resume == null)
            return NotFound();
            
        if (resume.UserId != GetCurrentUserId())
            return Forbid();
            
        return Ok(resume);
    }

    [HttpPost]
    public async Task<ActionResult<ResumeDto>> CreateResume(CreateResumeDto createResumeDto)
    {
        var userId = GetCurrentUserId();
        var resume = await resumeService.CreateResumeAsync(userId, createResumeDto);
        
        return CreatedAtAction(nameof(GetResumeById), new { id = resume.Id }, resume);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResumeDto>> UpdateResume(int id, UpdateResumeDto updateResumeDto)
    {
        try
        {
            // Get the resume to verify ownership
            var existingResume = await resumeService.GetResumeByIdAsync(id);
            if (existingResume == null)
                return NotFound();
                
            if (existingResume.UserId != GetCurrentUserId())
                return Forbid();
            
            var resume = await resumeService.UpdateResumeAsync(id, updateResumeDto);
            return Ok(resume);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteResume(int id)
    {
        try
        {
            var existingResume = await resumeService.GetResumeByIdAsync(id);
            if (existingResume == null)
                return NotFound();
                
            if (existingResume.UserId != GetCurrentUserId())
                return Forbid();
            
            await resumeService.DeleteResumeAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    private int GetCurrentUserId()
    {
        // In a real application, this would get the user ID from the claims principal
        // For now, we'll return a dummy value
        return 1;
    }
}