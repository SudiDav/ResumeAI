using AutoMapper;
using ResumeAI.Application.DTOs;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Interfaces;

namespace ResumeAI.Application.Services;

public class ResumeService(IUnitOfWork unitOfWork, IMapper mapper) : IResumeService
{
    public async Task<IEnumerable<ResumeDto>> GetAllResumesAsync(int userId)
    {
        var resumes = await unitOfWork.Resumes.GetResumesByUserIdAsync(userId);
        return mapper.Map<IEnumerable<ResumeDto>>(resumes);
    }

    public async Task<ResumeDto?> GetResumeByIdAsync(int id)
    {
        var resume = await unitOfWork.Resumes.GetResumeWithDetailsAsync(id);
        return resume != null ? mapper.Map<ResumeDto>(resume) : null;
    }

    public async Task<ResumeDto> CreateResumeAsync(int userId, CreateResumeDto createResumeDto)
    {
        var resume = mapper.Map<Resume>(createResumeDto);
        resume.UserId = userId;
        resume.CreatedAt = DateTime.UtcNow;
        
        await unitOfWork.Resumes.AddAsync(resume);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<ResumeDto>(resume);
    }

    public async Task<ResumeDto> UpdateResumeAsync(int id, UpdateResumeDto updateResumeDto)
    {
        var resume = await unitOfWork.Resumes.GetResumeWithDetailsAsync(id) 
            ?? throw new KeyNotFoundException($"Resume with ID {id} not found");
        
        mapper.Map(updateResumeDto, resume);
        resume.UpdatedAt = DateTime.UtcNow;
        
        await unitOfWork.Resumes.UpdateAsync(resume);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<ResumeDto>(resume);
    }

    public async Task DeleteResumeAsync(int id)
    {
        await unitOfWork.Resumes.DeleteAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}