using AutoMapper;
using ResumeAI.Application.DTOs;
using ResumeAI.Domain.Entities;

namespace ResumeAI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Resume mappings
        CreateMap<Resume, ResumeDto>();
        CreateMap<CreateResumeDto, Resume>();
        CreateMap<UpdateResumeDto, Resume>();
            
        // Experience mappings
        CreateMap<Experience, ExperienceDto>();
        CreateMap<CreateExperienceDto, Experience>();
        CreateMap<ExperienceDto, Experience>();
            
        // Education mappings
        CreateMap<Education, EducationDto>();
        CreateMap<CreateEducationDto, Education>();
        CreateMap<EducationDto, Education>();
            
        // UserProfile mappings
        CreateMap<UserProfile, UserProfileDto>();
        CreateMap<CreateUserProfileDto, UserProfile>();
        CreateMap<UpdateUserProfileDto, UserProfile>();
            
        // JobApplication mappings
        CreateMap<JobApplication, JobApplicationDto>();
        CreateMap<CreateJobApplicationDto, JobApplication>();
        CreateMap<UpdateJobApplicationDto, JobApplication>();
            
        // CoverLetter mappings
        CreateMap<CoverLetter, CoverLetterDto>();
        CreateMap<CreateCoverLetterDto, CoverLetter>();
        CreateMap<UpdateCoverLetterDto, CoverLetter>();
    }
}