using Microsoft.Extensions.DependencyInjection;
using ResumeAI.Application.Mappings;
using ResumeAI.Application.Services;

namespace ResumeAI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        
        // Register application services
        services.AddScoped<IResumeService, ResumeService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();
        services.AddScoped<ICoverLetterService, CoverLetterService>();
        services.AddScoped<IAIResumeService, AIResumeService>();
        
        return services;
    }
}