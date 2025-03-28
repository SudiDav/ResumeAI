using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeAI.Application.DTOs;
using ResumeAI.Application.Services;

namespace ResumeAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class JobApplicationController(IJobApplicationService jobApplicationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetAllJobApplications()
    {
        var userId = GetCurrentUserId();
        var jobApplications = await jobApplicationService.GetAllJobApplicationsAsync(userId);
        return Ok(jobApplications);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetJobApplicationsByStatus(string status)
    {
        var userId = GetCurrentUserId();
        var jobApplications = await jobApplicationService.GetJobApplicationsByStatusAsync(userId, status);
        return Ok(jobApplications);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplicationDto>> GetJobApplicationById(int id)
    {
        var jobApplication = await jobApplicationService.GetJobApplicationByIdAsync(id);
        
        if (jobApplication == null)
            return NotFound();
            
        // Verify ownership
        if (jobApplication.UserId != GetCurrentUserId())
            return Forbid();
            
        return Ok(jobApplication);
    }

    [HttpPost]
    public async Task<ActionResult<JobApplicationDto>> CreateJobApplication(CreateJobApplicationDto createJobApplicationDto)
    {
        var userId = GetCurrentUserId();
        var jobApplication = await jobApplicationService.CreateJobApplicationAsync(userId, createJobApplicationDto);
        
        return CreatedAtAction(nameof(GetJobApplicationById), new { id = jobApplication.Id }, jobApplication);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<JobApplicationDto>> UpdateJobApplication(int id, UpdateJobApplicationDto updateJobApplicationDto)
    {
        try
        {
            // Get the job application to verify ownership
            var existingJobApplication = await jobApplicationService.GetJobApplicationByIdAsync(id);
            if (existingJobApplication == null)
                return NotFound();
                
            if (existingJobApplication.UserId != GetCurrentUserId())
                return Forbid();
            
            var jobApplication = await jobApplicationService.UpdateJobApplicationAsync(id, updateJobApplicationDto);
            return Ok(jobApplication);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/status/{status}")]
    public async Task<ActionResult<JobApplicationDto>> UpdateJobApplicationStatus(int id, string status)
    {
        try
        {
            // Get the job application to verify ownership
            var existingJobApplication = await jobApplicationService.GetJobApplicationByIdAsync(id);
            if (existingJobApplication == null)
                return NotFound();
                
            if (existingJobApplication.UserId != GetCurrentUserId())
                return Forbid();
            
            var jobApplication = await jobApplicationService.UpdateJobApplicationStatusAsync(id, status);
            return Ok(jobApplication);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteJobApplication(int id)
    {
        try
        {
            // Get the job application to verify ownership
            var existingJobApplication = await jobApplicationService.GetJobApplicationByIdAsync(id);
            if (existingJobApplication == null)
                return NotFound();
                
            if (existingJobApplication.UserId != GetCurrentUserId())
                return Forbid();
            
            await jobApplicationService.DeleteJobApplicationAsync(id);
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