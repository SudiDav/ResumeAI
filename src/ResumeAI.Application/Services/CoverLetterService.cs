using AutoMapper;
using ResumeAI.Application.DTOs;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Interfaces;
using ResumeAI.Domain.Services;

namespace ResumeAI.Application.Services;

public class CoverLetterService(IUnitOfWork unitOfWork, IMapper mapper, IAIService aiService)
    : ICoverLetterService
{
    public async Task<IEnumerable<CoverLetterDto>> GetAllCoverLettersAsync(int userId)
    {
        var coverLetters = await unitOfWork.CoverLetters.GetCoverLettersByUserIdAsync(userId);
        return mapper.Map<IEnumerable<CoverLetterDto>>(coverLetters);
    }

    public async Task<IEnumerable<CoverLetterDto>> GetCoverLettersByResumeAsync(int resumeId)
    {
        var coverLetters = await unitOfWork.CoverLetters.GetCoverLettersByResumeIdAsync(resumeId);
        return mapper.Map<IEnumerable<CoverLetterDto>>(coverLetters);
    }

    public async Task<CoverLetterDto?> GetCoverLetterByIdAsync(int id)
    {
        var coverLetter = await unitOfWork.CoverLetters.GetByIdAsync(id);
        return coverLetter != null ? mapper.Map<CoverLetterDto>(coverLetter) : null;
    }

    public async Task<CoverLetterDto> CreateCoverLetterAsync(int userId, CreateCoverLetterDto createCoverLetterDto)
    {
        var coverLetter = mapper.Map<CoverLetter>(createCoverLetterDto);
        coverLetter.UserId = userId;
        coverLetter.CreatedAt = DateTime.UtcNow;
        
        await unitOfWork.CoverLetters.AddAsync(coverLetter);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<CoverLetterDto>(coverLetter);
    }

    public async Task<CoverLetterDto> UpdateCoverLetterAsync(int id, UpdateCoverLetterDto updateCoverLetterDto)
    {
        var coverLetter = await unitOfWork.CoverLetters.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Cover letter with ID {id} not found");
        
        mapper.Map(updateCoverLetterDto, coverLetter);
        coverLetter.UpdatedAt = DateTime.UtcNow;
        
        await unitOfWork.CoverLetters.UpdateAsync(coverLetter);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<CoverLetterDto>(coverLetter);
    }

    public async Task<CoverLetterDto> GenerateCoverLetterAsync(int userId, GenerateCoverLetterDto generateCoverLetterDto)
    {
        // Get the resume
        var resume = await unitOfWork.Resumes.GetResumeWithDetailsAsync(generateCoverLetterDto.ResumeId)
            ?? throw new KeyNotFoundException($"Resume with ID {generateCoverLetterDto.ResumeId} not found");
        
        // Create job description string
        var jobDescription = $"Job Title: {generateCoverLetterDto.JobTitle}\nCompany: {generateCoverLetterDto.Company}\nDescription: {generateCoverLetterDto.JobDescription}";
        
        // Generate cover letter using AI service
        var coverLetterContent = await aiService.GenerateCoverLetterAsync(resume, jobDescription);
        
        // Create cover letter entity
        var coverLetter = new CoverLetter
        {
            UserId = userId,
            ResumeId = generateCoverLetterDto.ResumeId,
            Title = $"Cover Letter for {generateCoverLetterDto.JobTitle} at {generateCoverLetterDto.Company}",
            Content = coverLetterContent,
            CreatedAt = DateTime.UtcNow
        };
        
        await unitOfWork.CoverLetters.AddAsync(coverLetter);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<CoverLetterDto>(coverLetter);
    }

    public async Task DeleteCoverLetterAsync(int id)
    {
        await unitOfWork.CoverLetters.DeleteAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}