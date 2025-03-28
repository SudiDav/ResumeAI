using ResumeAI.Application.DTOs;

namespace ResumeAI.Application.Services;

public interface ICoverLetterService
{
    Task<IEnumerable<CoverLetterDto>> GetAllCoverLettersAsync(int userId);
    Task<IEnumerable<CoverLetterDto>> GetCoverLettersByResumeAsync(int resumeId);
    Task<CoverLetterDto?> GetCoverLetterByIdAsync(int id);
    Task<CoverLetterDto> CreateCoverLetterAsync(int userId, CreateCoverLetterDto createCoverLetterDto);
    Task<CoverLetterDto> UpdateCoverLetterAsync(int id, UpdateCoverLetterDto updateCoverLetterDto);
    Task<CoverLetterDto> GenerateCoverLetterAsync(int userId, GenerateCoverLetterDto generateCoverLetterDto);
    Task DeleteCoverLetterAsync(int id);
}