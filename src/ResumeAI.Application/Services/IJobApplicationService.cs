using ResumeAI.Application.DTOs;

namespace ResumeAI.Application.Services;

public interface IJobApplicationService
{
    Task<IEnumerable<JobApplicationDto>> GetAllJobApplicationsAsync(int userId);
    Task<IEnumerable<JobApplicationDto>> GetJobApplicationsByStatusAsync(int userId, string status);
    Task<JobApplicationDto?> GetJobApplicationByIdAsync(int id);
    Task<JobApplicationDto> CreateJobApplicationAsync(int userId, CreateJobApplicationDto createJobApplicationDto);
    Task<JobApplicationDto> UpdateJobApplicationAsync(int id, UpdateJobApplicationDto updateJobApplicationDto);
    Task<JobApplicationDto> UpdateJobApplicationStatusAsync(int id, string status);
    Task DeleteJobApplicationAsync(int id);
}