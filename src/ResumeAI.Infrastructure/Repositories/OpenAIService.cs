using System.Text;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using ResumeAI.Domain.Entities;
using ResumeAI.Domain.Services;

namespace ResumeAI.Infrastructure.Services
{
    public class OpenAIService(IConfiguration configuration) : IAIService
    {
        private readonly string _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API key is not configured");
        private readonly string _model = "gpt-4";

        public async Task<string> GenerateImprovedResumeAsync(Resume resume, string jobDescription)
        {
            var resumeText = SerializeResumeToText(resume);
            
            var client = new ChatClient(_model, _apiKey);
            
            var messages = new ChatMessage[]
            {
                ChatMessage.CreateSystemMessage("You are a professional resume writer. Your task is to improve the resume to better match the job description."),
                ChatMessage.CreateUserMessage($"Here's my current resume:\n\n{resumeText}\n\nHere's the job description:\n\n{jobDescription}\n\nPlease improve my resume to better match this job. Maintain the same format but enhance the content.")
            };
            
            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f
            };
            
            // Add max tokens if the property exists in ChatCompletionOptions
            // (Check if this property exists; if not, the model will generate a reasonable length)
            
            var result = await client.CompleteChatAsync(messages, options);
            return result.Value.ToString();
        }

        public async Task<string> GenerateCoverLetterAsync(Resume resume, string jobDescription)
        {
            var resumeText = SerializeResumeToText(resume);
            
            var client = new ChatClient(_model, _apiKey);
            
            var messages = new ChatMessage[]
            {
                ChatMessage.CreateSystemMessage("You are a professional cover letter writer. Your task is to create a compelling cover letter based on the resume and job description."),
                ChatMessage.CreateUserMessage($"Here's my resume:\n\n{resumeText}\n\nHere's the job description:\n\n{jobDescription}\n\nPlease write a professional cover letter for this job application.")
            };
            
            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f
            };
            
            var result = await client.CompleteChatAsync(messages, options);
            return result.Value.ToString();
        }

        public async Task<List<string>> SuggestSkillImprovementsAsync(Resume resume, string jobDescription)
        {
            var resumeText = SerializeResumeToText(resume);
            
            var client = new ChatClient(_model, _apiKey);
            
            var messages = new ChatMessage[]
            {
                ChatMessage.CreateSystemMessage("You are a professional career advisor. Your task is to suggest skills that would improve this resume for the specific job."),
                ChatMessage.CreateUserMessage($"Here's my resume:\n\n{resumeText}\n\nHere's the job description:\n\n{jobDescription}\n\nPlease suggest 5-7 skills I should develop or highlight to better match this job. For each skill, provide a brief explanation of why it's important.")
            };
            
            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f
            };
            
            var result = await client.CompleteChatAsync(messages, options);
            var content = result.Value.ToString();
            
            // Parse the response to extract skills
            var skills = new List<string>();
            var lines = content.Split('\n');
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("- ") || trimmedLine.StartsWith("* ") || 
                    (trimmedLine.Length > 2 && char.IsDigit(trimmedLine[0]) && trimmedLine[1] == '.'))
                {
                    skills.Add(trimmedLine);
                }
            }
            
            return skills.Count > 0 ? skills : new List<string> { content };
        }

        public async Task<string> AnalyzeResumeAsync(Resume resume)
        {
            var resumeText = SerializeResumeToText(resume);
            
            var client = new ChatClient(_model, _apiKey);
            
            var messages = new ChatMessage[]
            {
                ChatMessage.CreateSystemMessage("You are a professional resume reviewer. Your task is to analyze the resume and provide constructive feedback."),
                ChatMessage.CreateUserMessage($"Here's my resume:\n\n{resumeText}\n\nPlease analyze my resume and provide feedback on its strengths, weaknesses, and suggestions for improvement.")
            };
            
            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f
            };
            
            var result = await client.CompleteChatAsync(messages, options);
            return result.Value.ToString();
        }

        private string SerializeResumeToText(Resume resume)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"# {resume.FullName}");
            sb.AppendLine($"Email: {resume.Email}");
            sb.AppendLine($"Phone: {resume.Phone}");
            sb.AppendLine();
            
            sb.AppendLine("## Summary");
            sb.AppendLine(resume.Summary);
            sb.AppendLine();
            
            sb.AppendLine("## Skills");
            foreach (var skill in resume.Skills)
            {
                sb.AppendLine($"- {skill}");
            }
            sb.AppendLine();
            
            sb.AppendLine("## Work Experience");
            foreach (var exp in resume.WorkExperiences)
            {
                sb.AppendLine($"### {exp.JobTitle} at {exp.Company}");
                sb.AppendLine($"{exp.Location} | {exp.StartDate:MMM yyyy} - {(exp.EndDate.HasValue ? exp.EndDate.Value.ToString("MMM yyyy") : "Present")}");
                sb.AppendLine(exp.Responsibilities);
                sb.AppendLine();
            }
            
            sb.AppendLine("## Education");
            foreach (var edu in resume.EducationHistory)
            {
                sb.AppendLine($"### {edu.Degree}");
                sb.AppendLine($"{edu.University} | {edu.GraduationYear}");
                sb.AppendLine();
            }
            
            return sb.ToString();
        }
    }
}