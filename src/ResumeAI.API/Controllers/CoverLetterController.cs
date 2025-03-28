using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeAI.Application.DTOs;
using ResumeAI.Application.Services;

namespace ResumeAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class CoverLetterController(ICoverLetterService coverLetterService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoverLetterDto>>> GetAllCoverLetters()
    {
        var userId = GetCurrentUserId();
        var coverLetters = await coverLetterService.GetAllCoverLettersAsync(userId);
        return Ok(coverLetters);
    }

    [HttpGet("resume/{resumeId}")]
    public async Task<ActionResult<IEnumerable<CoverLetterDto>>> GetCoverLettersByResume(int resumeId)
    {
        var coverLetters = await coverLetterService.GetCoverLettersByResumeAsync(resumeId);
        return Ok(coverLetters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoverLetterDto>> GetCoverLetterById(int id)
    {
        var coverLetter = await coverLetterService.GetCoverLetterByIdAsync(id);
        
        if (coverLetter == null)
            return NotFound();
            
        // Verify ownership
        if (coverLetter.UserId != GetCurrentUserId())
            return Forbid();
            
        return Ok(coverLetter);
    }

    [HttpPost]
    public async Task<ActionResult<CoverLetterDto>> CreateCoverLetter(CreateCoverLetterDto createCoverLetterDto)
    {
        var userId = GetCurrentUserId();
        var coverLetter = await coverLetterService.CreateCoverLetterAsync(userId, createCoverLetterDto);
        
        return CreatedAtAction(nameof(GetCoverLetterById), new { id = coverLetter.Id }, coverLetter);
    }

    [HttpPost("generate")]
    public async Task<ActionResult<CoverLetterDto>> GenerateCoverLetter(GenerateCoverLetterDto generateCoverLetterDto)
    {
        var userId = GetCurrentUserId();
        var coverLetter = await coverLetterService.GenerateCoverLetterAsync(userId, generateCoverLetterDto);
        
        return CreatedAtAction(nameof(GetCoverLetterById), new { id = coverLetter.Id }, coverLetter);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CoverLetterDto>> UpdateCoverLetter(int id, UpdateCoverLetterDto updateCoverLetterDto)
    {
        try
        {
            // Get the cover letter to verify ownership
            var existingCoverLetter = await coverLetterService.GetCoverLetterByIdAsync(id);
            if (existingCoverLetter == null)
                return NotFound();
                
            if (existingCoverLetter.UserId != GetCurrentUserId())
                return Forbid();
            
            var coverLetter = await coverLetterService.UpdateCoverLetterAsync(id, updateCoverLetterDto);
            return Ok(coverLetter);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCoverLetter(int id)
    {
        try
        {
            // Get the cover letter to verify ownership
            var existingCoverLetter = await coverLetterService.GetCoverLetterByIdAsync(id);
            if (existingCoverLetter == null)
                return NotFound();
                
            if (existingCoverLetter.UserId != GetCurrentUserId())
                return Forbid();
            
            await coverLetterService.DeleteCoverLetterAsync(id);
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