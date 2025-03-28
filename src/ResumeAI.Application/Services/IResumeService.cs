using ResumeAI.Application.DTOs;

namespace ResumeAI.Application.Services;

public interface IResumeService
{
    Task<IEnumerable<ResumeDto>> GetAllResumesAsync(int userId);
    Task<ResumeDto?> GetResumeByIdAsync(int id);
    Task<ResumeDto> CreateResumeAsync(int userId, CreateResumeDto createResumeDto);
    Task<ResumeDto> UpdateResumeAsync(int id, UpdateResumeDto updateResumeDto);
    Task DeleteResumeAsync(int id);
}