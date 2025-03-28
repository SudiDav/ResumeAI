using Microsoft.AspNetCore.Mvc;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Services;

namespace ResumeAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(IAIService aiService) : ControllerBase
{
    [HttpPost("analyze-sample")]
    public async Task<IActionResult> AnalyzeSampleResume()
    {
        try
        {
            var sampleResume = new Resume
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Phone = "555-123-4567",
                Summary = "Software developer with 5 years of experience in web development, particularly with .NET and JavaScript frameworks.",
                Skills = ["C#", "ASP.NET Core", "JavaScript", "Vue.js", "SQL", "Git"],
                WorkExperiences =
                [
                    new()
                    {
                        JobTitle = "Senior Software Developer",
                        Company = "Tech Innovations Inc.",
                        Location = "Seattle, WA",
                        StartDate = new DateTime(2020, 6, 1),
                        EndDate = null,
                        Responsibilities =
                            "Developed and maintained web applications using ASP.NET Core and Vue.js. Led a team of 3 developers. Implemented CI/CD pipelines."
                    },

                    new()
                    {
                        JobTitle = "Software Developer",
                        Company = "Digital Solutions LLC",
                        Location = "Portland, OR",
                        StartDate = new DateTime(2018, 3, 1),
                        EndDate = new DateTime(2020, 5, 31),
                        Responsibilities =
                            "Built and maintained RESTful APIs using ASP.NET Core. Worked on front-end development with Vue.js and Bootstrap."
                    }
                ],
                EducationHistory =
                [
                    new Education
                    {
                        Degree = "Bachelor of Science in Computer Science",
                        University = "University of Washington",
                        GraduationYear = 2018
                    }
                ]
            };

            var analysis = await aiService.AnalyzeResumeAsync(sampleResume);
                
            return Ok(new { Analysis = analysis });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message, StackTrace = ex.StackTrace });
        }
    }
}