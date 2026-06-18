using System.Text.Json;
using System.Text.Json.Serialization;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using RecruitmentPlatformAPI.Enums;
using RecruitmentPlatformAPI.Models.Assessment.V2;
using RecruitmentPlatformAPI.Models.Identity;
using RecruitmentPlatformAPI.Models.JobSeeker;
using RecruitmentPlatformAPI.Models.Notification;
using RecruitmentPlatformAPI.Models.Reference;

namespace RecruitmentPlatformAPI.Data.Seed
{
    public static class JobSeekerDataSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IWebHostEnvironment env, ILogger logger)
        {
            try
            {
                var existingCount = await context.Users.CountAsync(u => u.AccountType == AccountType.JobSeeker);
                if (existingCount > 0)
                {
                    logger.LogInformation("Job seeker seed data already exists ({Count} users). Skipping.", existingCount);
                    return;
                }

                var filePath = Path.Combine(env.ContentRootPath, "Data", "SeedData", "jobseekers.json");
                if (!File.Exists(filePath))
                {
                    logger.LogWarning("jobseekers.json not found at {Path}. Skipping job seeker seeding.", filePath);
                    return;
                }

                var json = await File.ReadAllTextAsync(filePath);
                var profiles = JsonSerializer.Deserialize<List<JobSeekerSeedProfile>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (profiles == null || profiles.Count == 0)
                {
                    logger.LogWarning("jobseekers.json is empty or invalid. Skipping.");
                    return;
                }

                logger.LogInformation("Seeding {Count} job seeker profiles...", profiles.Count);

                var defaultPasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!");
                var now = DateTime.UtcNow;

                foreach (var profile in profiles)
                {
                    var user = new User
                    {
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Email = profile.Email,
                        PasswordHash = defaultPasswordHash,
                        AuthProvider = AuthProvider.Email,
                        AccountType = AccountType.JobSeeker,
                        IsEmailVerified = true,
                        IsActive = true,
                        ProfileCompletionStep = profile.ProfileCompletionStep,
                        CreatedAt = now,
                        UpdatedAt = now
                    };
                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    var userSettings = new UserSettings
                    {
                        UserId = user.Id,
                        EmailNotificationsEnabled = true,
                        WeeklyDigestEnabled = true,
                        NewCandidateAlertsEnabled = true,
                        CreatedAt = now,
                        UpdatedAt = now
                    };
                    context.UserSettings.Add(userSettings);

                    var jobSeeker = new JobSeeker
                    {
                        UserId = user.Id,
                        JobTitleId = profile.JobTitleId,
                        YearsOfExperience = profile.YearsOfExperience,
                        CountryId = profile.CountryId,
                        CityId = profile.CityId,
                        PhoneNumber = profile.Phone,
                        FirstLanguageId = profile.FirstLanguageId,
                        FirstLanguageProficiency = profile.FirstLanguageProficiency,
                        SecondLanguageId = profile.SecondLanguageId,
                        SecondLanguageProficiency = profile.SecondLanguageProficiency,
                        Bio = profile.Bio,
                        CreatedAt = now,
                        UpdatedAt = now,
                        WorkPreferences = profile.WorkPreferences ?? new List<WorkModel>(),
                        DesiredEmploymentTypes = profile.DesiredEmploymentTypes ?? new List<EmploymentType>()
                    };
                    context.JobSeekers.Add(jobSeeker);
                    await context.SaveChangesAsync();

                    if (profile.Education != null)
                    {
                        for (int i = 0; i < profile.Education.Count; i++)
                        {
                            var e = profile.Education[i];
                            context.Educations.Add(new Education
                            {
                                JobSeekerId = jobSeeker.Id,
                                Institution = e.Institution,
                                Degree = e.Degree,
                                FieldOfStudyId = e.FieldOfStudyId,
                                FieldOfStudyName = e.FieldOfStudyName,
                                GradeOrGPA = e.GradeOrGPA,
                                StartDate = e.StartDate,
                                EndDate = e.EndDate,
                                IsCurrent = e.IsCurrent,
                                DisplayOrder = i,
                                CreatedAt = now,
                                UpdatedAt = now
                            });
                        }
                    }

                    if (profile.Experience != null)
                    {
                        for (int i = 0; i < profile.Experience.Count; i++)
                        {
                            var exp = profile.Experience[i];
                            context.Experiences.Add(new Experience
                            {
                                JobSeekerId = jobSeeker.Id,
                                JobTitle = exp.JobTitle,
                                CompanyName = exp.CompanyName,
                                CountryId = exp.CountryId,
                                CityId = exp.CityId,
                                EmploymentType = exp.EmploymentType,
                                StartDate = exp.StartDate,
                                EndDate = exp.EndDate,
                                IsCurrent = exp.IsCurrent,
                                Responsibilities = exp.Responsibilities,
                                DisplayOrder = i,
                                CreatedAt = now,
                                UpdatedAt = now
                            });
                        }
                    }

                    if (profile.Projects != null)
                    {
                        for (int i = 0; i < profile.Projects.Count; i++)
                        {
                            var p = profile.Projects[i];
                            context.Projects.Add(new Project
                            {
                                JobSeekerId = jobSeeker.Id,
                                Title = p.Title,
                                Description = p.Description,
                                TechnologiesUsed = p.TechnologiesUsed,
                                ProjectLink = p.ProjectLink,
                                DisplayOrder = i,
                                CreatedAt = now,
                                UpdatedAt = now
                            });
                        }
                    }

                    if (profile.Skills != null)
                    {
                        foreach (var skillId in profile.Skills)
                        {
                            context.JobSeekerSkills.Add(new JobSeekerSkill
                            {
                                JobSeekerId = jobSeeker.Id,
                                SkillId = skillId,
                                Source = "Self"
                            });
                        }
                    }

                    context.SocialAccounts.Add(new SocialAccount
                    {
                        JobSeekerId = jobSeeker.Id,
                        LinkedIn = profile.SocialLinks?.LinkedIn,
                        Github = profile.SocialLinks?.GitHub,
                        PersonalWebsite = profile.SocialLinks?.PersonalWebsite,
                        CreatedAt = now,
                        UpdatedAt = now
                    });

                    if (profile.Certificates != null)
                    {
                        for (int i = 0; i < profile.Certificates.Count; i++)
                        {
                            var c = profile.Certificates[i];
                            context.Certificates.Add(new Certificate
                            {
                                JobSeekerId = jobSeeker.Id,
                                Title = c.Title,
                                IssuingOrganization = c.IssuingOrganization,
                                IssueDate = c.IssueDate,
                                ExpirationDate = c.ExpirationDate,
                                DisplayOrder = i,
                                CreatedAt = now,
                                UpdatedAt = now
                            });
                        }
                    }

                    if (profile.Assessment != null && profile.Assessment.Status == 2)
                    {
                        var assessmentDate = now.AddDays(-Random.Shared.Next(1, 500));
                        var assessment = new AssessmentAttemptV2
                        {
                            JobSeekerId = jobSeeker.Id,
                            JobTitleId = profile.JobTitleId,
                            QuestionIdsJson = "[]",
                            ClaimedSkillIdsJson = "[]",
                            TotalQuestions = 20,
                            QuestionsAnswered = 20,
                            ResumeCount = 0,
                            TechnicalScore = profile.Assessment.TechnicalScore,
                            SoftSkillsScore = profile.Assessment.SoftSkillsScore,
                            OverallScore = profile.Assessment.OverallScore,
                            TimeLimitMinutes = 20,
                            Status = 2,
                            RetakeNumber = 1,
                            IsActive = true,
                            StartedAt = assessmentDate,
                            CompletedAt = assessmentDate.AddMinutes(18),
                            ExpiresAt = assessmentDate.AddMinutes(20),
                            ScoreExpiresAt = assessmentDate.AddMonths(18)
                        };
                        context.AssessmentAttemptsV2.Add(assessment);
                        await context.SaveChangesAsync();

                        jobSeeker.CurrentAssessmentScore = profile.Assessment.OverallScore;
                        jobSeeker.LastAssessmentDate = assessmentDate;
                        jobSeeker.AssessmentJobTitleId = profile.JobTitleId;
                    }

                    if (profile.Notifications != null)
                    {
                        foreach (var n in profile.Notifications)
                        {
                            context.Notifications.Add(new Notification
                            {
                                UserId = user.Id,
                                Type = n.Type,
                                Title = n.Title,
                                Message = n.Message,
                                SenderName = n.SenderName,
                                IsRead = n.IsRead,
                                CreatedAt = now.AddDays(-Random.Shared.Next(1, 30))
                            });
                        }
                    }

                    await context.SaveChangesAsync();
                }

                logger.LogInformation("Successfully seeded {Count} job seeker profiles.", profiles.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed job seeker data.");
            }
        }

        public class JobSeekerSeedProfile
        {
            public string FirstName { get; set; } = "";
            public string LastName { get; set; } = "";
            public string Email { get; set; } = "";
            public string? Phone { get; set; }
            public int CountryId { get; set; }
            public int CityId { get; set; }
            public int JobTitleId { get; set; }
            public int YearsOfExperience { get; set; }
            public string? Bio { get; set; }
            public int FirstLanguageId { get; set; }
            public LanguageProficiency FirstLanguageProficiency { get; set; }
            public int? SecondLanguageId { get; set; }
            public LanguageProficiency? SecondLanguageProficiency { get; set; }
            public List<WorkModel>? WorkPreferences { get; set; }
            public List<EmploymentType>? DesiredEmploymentTypes { get; set; }
            public int ProfileCompletionStep { get; set; } = 4;
            public List<SeedEducation>? Education { get; set; }
            public List<SeedExperience>? Experience { get; set; }
            public List<SeedProject>? Projects { get; set; }
            public List<int>? Skills { get; set; }
            public SeedSocialLinks? SocialLinks { get; set; }
            public List<SeedCertificate>? Certificates { get; set; }
            public SeedAssessment? Assessment { get; set; }
            public List<SeedNotification>? Notifications { get; set; }
        }

        public class SeedEducation
        {
            public string Institution { get; set; } = "";
            public Degree Degree { get; set; }
            public int? FieldOfStudyId { get; set; }
            public string? FieldOfStudyName { get; set; }
            public string? GradeOrGPA { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool IsCurrent { get; set; }
        }

        public class SeedExperience
        {
            public string JobTitle { get; set; } = "";
            public string CompanyName { get; set; } = "";
            public int? CountryId { get; set; }
            public int? CityId { get; set; }
            public EmploymentType EmploymentType { get; set; } = EmploymentType.FullTime;
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool IsCurrent { get; set; }
            public string? Responsibilities { get; set; }
        }

        public class SeedProject
        {
            public string Title { get; set; } = "";
            public string? Description { get; set; }
            public string? TechnologiesUsed { get; set; }
            public string? ProjectLink { get; set; }
        }

        public class SeedSocialLinks
        {
            public string? LinkedIn { get; set; }
            public string? GitHub { get; set; }
            public string? PersonalWebsite { get; set; }
        }

        public class SeedCertificate
        {
            public string Title { get; set; } = "";
            public string? IssuingOrganization { get; set; }
            public DateTime? IssueDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
        }

        public class SeedAssessment
        {
            public decimal TechnicalScore { get; set; }
            public decimal SoftSkillsScore { get; set; }
            public decimal OverallScore { get; set; }
            public int Status { get; set; }
        }

        public class SeedNotification
        {
            public string Type { get; set; } = "";
            public string Title { get; set; } = "";
            public string Message { get; set; } = "";
            public string? SenderName { get; set; }
            public bool IsRead { get; set; }
        }
    }
}
