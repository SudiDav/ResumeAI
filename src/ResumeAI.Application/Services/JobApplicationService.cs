using AutoMapper;
using ResumeAI.Application.DTOs;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Enums;
using ResumeAI.Domain.Interfaces;

namespace ResumeAI.Application.Services;

public class JobApplicationService(IUnitOfWork unitOfWork, IMapper mapper) : IJobApplicationService
{
    public async Task<IEnumerable<JobApplicationDto>> GetAllJobApplicationsAsync(int userId)
    {
        var jobApplications = await unitOfWork.JobApplications.GetApplicationsByUserIdAsync(userId);
        return mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);
    }

    public async Task<IEnumerable<JobApplicationDto>> GetJobApplicationsByStatusAsync(int userId, string status)
    {
        var jobApplications = await unitOfWork.JobApplications.GetApplicationsByStatusAsync(userId, status);
        return mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);
    }

    public async Task<JobApplicationDto?> GetJobApplicationByIdAsync(int id)
    {
        var jobApplication = await unitOfWork.JobApplications.GetByIdAsync(id);
        return jobApplication != null ? mapper.Map<JobApplicationDto>(jobApplication) : null;
    }

    public async Task<JobApplicationDto> CreateJobApplicationAsync(int userId, CreateJobApplicationDto createJobApplicationDto)
    {
        var jobApplication = mapper.Map<JobApplication>(createJobApplicationDto);
        jobApplication.UserId = userId;
        jobApplication.AppliedOn = DateTime.UtcNow;
        jobApplication.ApplicationStatus = ApplicationStatus.Hired;
        
        await unitOfWork.JobApplications.AddAsync(jobApplication);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<JobApplicationDto>(jobApplication);
    }

    public async Task<JobApplicationDto> UpdateJobApplicationAsync(int id, UpdateJobApplicationDto updateJobApplicationDto)
    {
        var jobApplication = await unitOfWork.JobApplications.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Job application with ID {id} not found");
        
        mapper.Map(updateJobApplicationDto, jobApplication);
        jobApplication.LastUpdated = DateTime.UtcNow;
        
        await unitOfWork.JobApplications.UpdateAsync(jobApplication);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<JobApplicationDto>(jobApplication);
    }

    public async Task<JobApplicationDto> UpdateJobApplicationStatusAsync(int id, string status)
    {
        var jobApplication = await unitOfWork.JobApplications.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Job application with ID {id} not found");
        
        jobApplication.ApplicationStatus = ApplicationStatus.Pending;
        jobApplication.LastUpdated = DateTime.UtcNow;
        
        await unitOfWork.JobApplications.UpdateAsync(jobApplication);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<JobApplicationDto>(jobApplication);
    }

    public async Task DeleteJobApplicationAsync(int id)
    {
        await unitOfWork.JobApplications.DeleteAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}