using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecruitmentPlatformAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldOfStudy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOfStudy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RoleFamily = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aliases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthProvider = table.Column<int>(type: "int", nullable: false),
                    ProviderUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    LastFailedLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSuccessfulLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfileCompletionStep = table.Column<int>(type: "int", nullable: false),
                    LastWeeklyDigestSentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentQuestionsV2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "int", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeniorityLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimePerQuestion = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentQuestionsV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentQuestionsV2_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailVerifications_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RelatedEntityId = table.Column<int>(type: "int", nullable: true),
                    RelatedEntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SenderName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SenderPictureUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResets_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EmailNotificationsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    WeeklyDigestEnabled = table.Column<bool>(type: "bit", nullable: false),
                    NewCandidateAlertsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FirstLanguageId = table.Column<int>(type: "int", nullable: true),
                    FirstLanguageProficiency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLanguageId = table.Column<int>(type: "int", nullable: true),
                    SecondLanguageProficiency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkPreferences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesiredEmploymentTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentAssessmentScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    LastAssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssessmentJobTitleId = table.Column<int>(type: "int", nullable: true),
                    LastJobTitleChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekers_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSeekers_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSeekers_JobTitle_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSeekers_Language_FirstLanguageId",
                        column: x => x.FirstLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSeekers_Language_SecondLanguageId",
                        column: x => x.SecondLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobSeekers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompanySize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LinkedIn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CompanyDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastCompanyNameChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruiters_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruiters_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruiters_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentAttemptsV2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    QuestionIdsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimedSkillIdsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    QuestionsAnswered = table.Column<int>(type: "int", nullable: false),
                    ResumeCount = table.Column<int>(type: "int", nullable: false),
                    TechnicalScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    SoftSkillsScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    OverallScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    TimeLimitMinutes = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RetakeNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentAttemptsV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentAttemptsV2_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentAttemptsV2_JobTitle_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IssuingOrganization = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StoredFileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.CheckConstraint("CK_Certificate_ExpirationDateAfterIssueDate", "[ExpirationDate] IS NULL OR [IssueDate] IS NULL OR [ExpirationDate] >= [IssueDate]");
                    table.ForeignKey(
                        name: "FK_Certificates_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FieldOfStudyId = table.Column<int>(type: "int", nullable: true),
                    FieldOfStudyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GradeOrGPA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.CheckConstraint("CK_Education_EndDateAfterStartDate", "[EndDate] IS NULL OR [EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_Educations_FieldOfStudy_FieldOfStudyId",
                        column: x => x.FieldOfStudyId,
                        principalTable: "FieldOfStudy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Educations_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    EmploymentType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.CheckConstraint("CK_Experience_EndDateAfterStartDate", "[EndDate] IS NULL OR [EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_Experiences_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experiences_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experiences_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TechnologiesUsed = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    ProjectLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    ParseStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Github = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Behance = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Dribbble = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PersonalWebsite = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialAccounts_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecruiterId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinYearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    WorkModel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_JobTitle_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    ViewerRecruiterId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    ViewType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileView_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileView_Recruiters_ViewerRecruiterId",
                        column: x => x.ViewerRecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssessmentAnswersV2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentAttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedAnswerIndex = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    TimeSpentSeconds = table.Column<int>(type: "int", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentAnswersV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswersV2_AssessmentAttemptsV2_AssessmentAttemptId",
                        column: x => x.AssessmentAttemptId,
                        principalTable: "AssessmentAttemptsV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswersV2_AssessmentQuestionsV2_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AssessmentQuestionsV2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    MatchScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    AiReasoning = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MatchedSkillsJson = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MissingSkillsJson = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recommendations_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortlistedCandidate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    RecruiterId = table.Column<int>(type: "int", nullable: false),
                    ShortlistedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortlistedCandidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortlistedCandidate_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShortlistedCandidate_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShortlistedCandidate_Recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "JobTitle",
                columns: new[] { "Id", "Category", "CreatedAt", "IsActive", "RoleFamily", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مطور واجهات خلفية", "Backend Developer" },
                    { 2, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 1, "مطور واجهات أمامية", "Frontend Developer" },
                    { 3, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مطور شامل", "Full Stack Developer" },
                    { 4, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 4, "مطور تطبيقات الجوال", "Mobile Developer" },
                    { 5, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 4, "مطور iOS", "iOS Developer" },
                    { 6, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 4, "مطور أندرويد", "Android Developer" },
                    { 7, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس DevOps", "DevOps Engineer" },
                    { 8, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "عالم بيانات", "Data Scientist" },
                    { 9, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس بيانات", "Data Engineer" },
                    { 10, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس تعلم الآلة", "Machine Learning Engineer" },
                    { 11, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس ذكاء اصطناعي", "AI Engineer" },
                    { 12, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مهندس برمجيات", "Software Engineer" },
                    { 13, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 7, "مهندس ضمان الجودة", "QA Engineer" },
                    { 14, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 7, "مهندس أتمتة الاختبارات", "Test Automation Engineer" },
                    { 15, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس حوسبة سحابية", "Cloud Engineer" },
                    { 16, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس أمن المعلومات", "Security Engineer" },
                    { 17, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "محلل أمن سيبراني", "Cybersecurity Analyst" },
                    { 18, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس شبكات", "Network Engineer" },
                    { 19, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مسؤول أنظمة", "Systems Administrator" },
                    { 20, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مسؤول قواعد بيانات", "Database Administrator" },
                    { 21, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مهندس حلول", "Solutions Architect" },
                    { 22, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مهندس بنية تقنية", "Technical Architect" },
                    { 23, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس موثوقية الموقع", "Site Reliability Engineer" },
                    { 24, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مهندس أنظمة مدمجة", "Embedded Systems Engineer" },
                    { 25, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مطور ألعاب", "Game Developer" },
                    { 26, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مطور بلوك تشين", "Blockchain Developer" },
                    { 27, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مهندس إنترنت الأشياء", "IoT Engineer" },
                    { 28, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس رؤية حاسوبية", "Computer Vision Engineer" },
                    { 29, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس معالجة اللغات الطبيعية", "NLP Engineer" },
                    { 30, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "محلل ذكاء الأعمال", "Business Intelligence Analyst" },
                    { 31, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "محلل بيانات", "Data Analyst" },
                    { 32, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي دعم تقنية المعلومات", "IT Support Specialist" },
                    { 33, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مهندس دعم فني", "Technical Support Engineer" },
                    { 34, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تقنية المعلومات", "IT Manager" },
                    { 35, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للتكنولوجيا (CTO)", "CTO" },
                    { 36, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير هندسي", "Engineering Manager" },
                    { 37, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "قائد تقني", "Technical Lead" },
                    { 38, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "سكرم ماستر", "Scrum Master" },
                    { 39, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج", "Product Manager" },
                    { 40, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج تقني", "Technical Product Manager" },
                    { 41, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم تجربة المستخدم", "UX Designer" },
                    { 42, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم واجهة المستخدم", "UI Designer" },
                    { 43, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم UI/UX", "UX/UI Designer" },
                    { 44, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم جرافيك", "Graphic Designer" },
                    { 45, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم ويب", "Web Designer" },
                    { 46, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم مرئي", "Visual Designer" },
                    { 47, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم منتجات", "Product Designer" },
                    { 48, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم تفاعلي", "Interaction Designer" },
                    { 49, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم موشن جرافيك", "Motion Designer" },
                    { 50, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم ثلاثي الأبعاد", "3D Designer" },
                    { 51, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم ألعاب", "Game Designer" },
                    { 52, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "باحث تجربة المستخدم", "UX Researcher" },
                    { 53, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مدير إبداعي", "Creative Director" },
                    { 54, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مدير فني", "Art Director" },
                    { 55, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم هوية بصرية", "Brand Designer" },
                    { 56, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي تسويق رقمي", "Digital Marketing Specialist" },
                    { 57, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي SEO", "SEO Specialist" },
                    { 58, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تسويق بالمحتوى", "Content Marketing Manager" },
                    { 59, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير وسائل التواصل الاجتماعي", "Social Media Manager" },
                    { 60, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تسويق", "Marketing Manager" },
                    { 61, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير علامة تجارية", "Brand Manager" },
                    { 62, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير نمو", "Growth Manager" },
                    { 63, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي تسويق عبر البريد الإلكتروني", "Email Marketing Specialist" },
                    { 64, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل تسويق", "Marketing Analyst" },
                    { 65, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "كاتب محتوى", "Content Writer" },
                    { 66, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مندوب مبيعات", "Sales Representative" },
                    { 67, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مسؤول حسابات تنفيذي", "Account Executive" },
                    { 68, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مبيعات", "Sales Manager" },
                    { 69, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تطوير أعمال", "Business Development Manager" },
                    { 70, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير نجاح عملاء", "Customer Success Manager" },
                    { 71, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محاسب", "Accountant" },
                    { 72, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل مالي", "Financial Analyst" },
                    { 73, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مالي", "Finance Manager" },
                    { 74, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للمالية (CFO)", "CFO" },
                    { 75, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل استثمار", "Investment Analyst" },
                    { 76, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير موارد بشرية", "HR Manager" },
                    { 77, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي توظيف", "Recruiter" },
                    { 78, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي استقطاب مواهب", "Talent Acquisition Specialist" },
                    { 79, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "شريك أعمال موارد بشرية", "HR Business Partner" },
                    { 80, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات الأفراد", "People Operations Manager" },
                    { 81, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات", "Operations Manager" },
                    { 82, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مشروع", "Project Manager" },
                    { 83, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير برنامج", "Program Manager" },
                    { 84, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير سلاسل الإمداد", "Supply Chain Manager" },
                    { 85, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "منسق لوجستي", "Logistics Coordinator" },
                    { 86, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي (CEO)", "CEO" },
                    { 87, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للعمليات (COO)", "COO" },
                    { 88, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "نائب رئيس الهندسة", "VP of Engineering" },
                    { 89, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "نائب رئيس المنتجات", "VP of Product" },
                    { 90, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "نائب رئيس المبيعات", "VP of Sales" },
                    { 91, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس منصات", "Platform Engineer" },
                    { 92, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس بنية تحتية", "Infrastructure Engineer" },
                    { 93, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "معماري سحابي", "Cloud Architect" },
                    { 94, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "معماري أمن معلومات", "Security Architect" },
                    { 95, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مختبر اختراق", "Penetration Tester" },
                    { 96, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "محلل SOC", "SOC Analyst" },
                    { 97, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس MLOps", "MLOps Engineer" },
                    { 98, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "معماري بيانات", "Data Architect" },
                    { 99, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس تحليلات", "Analytics Engineer" },
                    { 100, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس أوامر (Prompt Engineer)", "Prompt Engineer" },
                    { 101, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مطور AR/VR", "AR/VR Developer" },
                    { 102, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مهندس برامج ثابتة", "Firmware Engineer" },
                    { 103, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مطور RPA", "RPA Developer" },
                    { 104, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مهندس Staff", "Staff Engineer" },
                    { 105, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مهندس رئيسي", "Principal Engineer" },
                    { 106, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "معماري برمجيات", "Software Architect" },
                    { 107, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مهندس تكامل أنظمة", "Integration Engineer" },
                    { 108, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مطور Salesforce", "Salesforce Developer" },
                    { 109, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مطور ERP", "ERP Developer" },
                    { 110, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مطور SharePoint", "SharePoint Developer" },
                    { 111, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مهندس API", "API Engineer" },
                    { 112, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "معماري خدمات مصغرة", "Microservices Architect" },
                    { 113, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مهندس قواعد بيانات", "Database Engineer" },
                    { 114, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, "مطور Web3", "Web3 Developer" },
                    { 115, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "معماري منصات", "Platform Architect" },
                    { 116, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "معماري حلول AWS", "AWS Solutions Architect" },
                    { 117, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس بناء وإصدار", "Build & Release Engineer" },
                    { 118, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مدير إصدارات", "Release Manager" },
                    { 119, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس مراقبة أنظمة", "Observability Engineer" },
                    { 120, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس أداء", "Performance Engineer" },
                    { 121, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مهندس أتمتة", "Automation Engineer" },
                    { 122, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "عالم أبحاث ذكاء اصطناعي", "AI Research Scientist" },
                    { 123, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 5, "مهندس أبحاث", "Research Engineer" },
                    { 124, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "محلل استخبارات التهديدات", "Threat Intelligence Analyst" },
                    { 125, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "محلل أدلة جنائية رقمية", "Digital Forensics Analyst" },
                    { 126, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "محلل استجابة للحوادث", "Incident Response Analyst" },
                    { 127, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "باحث ثغرات أمنية", "Vulnerability Researcher" },
                    { 128, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 6, "مدير عمليات أمنية", "Security Operations Manager" },
                    { 129, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مطور Low-Code/No-Code", "Low-Code/No-Code Developer" },
                    { 130, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير برنامج تقني", "Technical Program Manager" },
                    { 131, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "كاتب تجربة مستخدم", "UX Writer" },
                    { 132, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "قائد أنظمة تصميم", "Design Systems Lead" },
                    { 133, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "رسام توضيحي", "Illustrator" },
                    { 134, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "محرر فيديو", "Video Editor" },
                    { 135, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم وسائط متعددة", "Multimedia Designer" },
                    { 136, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم مطبوعات", "Print Designer" },
                    { 137, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم تغليف", "Packaging Designer" },
                    { 138, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم إمكانية الوصول", "Accessibility Designer" },
                    { 139, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مدير تصميم", "Design Director" },
                    { 140, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 1, "مهندس UI", "UI Engineer" },
                    { 141, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "مصمم تايبوجرافي", "Typographer" },
                    { 142, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تسويق بالأداء", "Performance Marketing Manager" },
                    { 143, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير توليد الطلب", "Demand Generation Manager" },
                    { 144, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "نائب رئيس التسويق", "VP of Marketing" },
                    { 145, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تسويق عبر المؤثرين", "Influencer Marketing Manager" },
                    { 146, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تسويق بالعمولة", "Affiliate Marketing Manager" },
                    { 147, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير علاقات عامة", "PR Manager" },
                    { 148, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير حملات تسويقية", "Campaign Manager" },
                    { 149, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات تسويق", "Marketing Operations Manager" },
                    { 150, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مجتمع", "Community Manager" },
                    { 151, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تسويق نمو", "Growth Marketing Manager" },
                    { 152, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "ممثل تطوير مبيعات (SDR)", "Sales Development Representative (SDR)" },
                    { 153, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "ممثل تطوير أعمال (BDR)", "Business Development Representative (BDR)" },
                    { 154, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مسؤول حسابات مؤسسية", "Enterprise Account Executive" },
                    { 155, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير حسابات رئيسية", "Key Account Manager" },
                    { 156, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مبيعات قنوات", "Channel Sales Manager" },
                    { 157, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات مبيعات", "Sales Operations Manager" },
                    { 158, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "ممثل مبيعات داخلية", "Inside Sales Representative" },
                    { 159, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مهندس ما قبل البيع", "Pre-Sales Engineer" },
                    { 160, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مهندس حلول", "Solutions Engineer" },
                    { 161, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير إيرادات", "Revenue Manager" },
                    { 162, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مراقب مالي", "Financial Controller" },
                    { 163, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير خزينة", "Treasury Manager" },
                    { 164, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي ضرائب", "Tax Specialist" },
                    { 165, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدقق حسابات", "Auditor" },
                    { 166, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل مخاطر", "Risk Analyst" },
                    { 167, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مسؤول امتثال", "Compliance Officer" },
                    { 168, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي رواتب", "Payroll Specialist" },
                    { 169, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل ميزانية", "Budget Analyst" },
                    { 170, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل كمي", "Quantitative Analyst" },
                    { 171, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تخطيط وتحليل مالي (FP&A)", "FP&A Manager" },
                    { 172, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تعلم وتطوير", "Learning & Development Manager" },
                    { 173, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تعويضات ومزايا", "Compensation & Benefits Manager" },
                    { 174, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير علاقات موظفين", "Employee Relations Manager" },
                    { 175, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي موارد بشرية عام", "HR Generalist" },
                    { 176, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل قوى عاملة", "Workforce Analyst" },
                    { 177, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي تهيئة موظفين", "Onboarding Specialist" },
                    { 178, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير إدارة موارد بشرية", "HR Director" },
                    { 179, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للموارد البشرية (CPO)", "Chief People Officer" },
                    { 180, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير ثقافة وتفاعل مؤسسي", "Culture & Engagement Manager" },
                    { 181, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي تطوير تنظيمي", "Organizational Development Specialist" },
                    { 182, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل أعمال", "Business Analyst" },
                    { 183, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تحسين عمليات", "Process Improvement Manager" },
                    { 184, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مشتريات", "Procurement Manager" },
                    { 185, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير موردين", "Vendor Manager" },
                    { 186, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير دعم عملاء", "Customer Support Manager" },
                    { 187, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي دعم عملاء", "Customer Support Specialist" },
                    { 188, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير إداري", "Administrative Manager" },
                    { 189, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "كبير موظفين (Chief of Staff)", "Chief of Staff" },
                    { 190, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير استراتيجية", "Strategy Manager" },
                    { 191, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير ERP", "ERP Manager" },
                    { 192, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات إيرادات", "Revenue Operations Manager" },
                    { 193, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات تقنية المعلومات", "IT Operations Manager" },
                    { 194, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تحول مؤسسي", "Transformation Manager" },
                    { 195, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مكتب", "Office Manager" },
                    { 196, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مرافق", "Facilities Manager" },
                    { 197, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج أول", "Senior Product Manager" },
                    { 198, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج رئيسي", "Principal Product Manager" },
                    { 199, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مجموعة منتجات", "Group Product Manager" },
                    { 200, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير إدارة منتجات", "Director of Product Management" },
                    { 201, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "رئيس قسم المنتجات", "Head of Product" },
                    { 202, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مالك منتج", "Product Owner" },
                    { 203, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مساعد مدير منتج", "Associate Product Manager" },
                    { 204, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عمليات المنتجات", "Product Operations Manager" },
                    { 205, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج منصة", "Platform Product Manager" },
                    { 206, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج نمو", "Growth Product Manager" },
                    { 207, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير منتج ذكاء اصطناعي", "AI Product Manager" },
                    { 208, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل منتجات", "Product Analyst" },
                    { 209, "Product Management", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مالك منتج تقني", "Technical Product Owner" },
                    { 210, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "صانع محتوى", "Content Creator" },
                    { 211, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "كاتب إعلانات", "Copywriter" },
                    { 212, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "كاتب تقني", "Technical Writer" },
                    { 213, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مصور فيديو", "Videographer" },
                    { 214, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "منتج بودكاست", "Podcast Producer" },
                    { 215, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "صانع محتوى تواصل اجتماعي", "Social Media Content Creator" },
                    { 216, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "منتج فيديو", "Video Producer" },
                    { 217, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "كاتب سيناريو", "Scriptwriter" },
                    { 218, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "استراتيجي محتوى", "Content Strategist" },
                    { 219, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محرر", "Editor" },
                    { 220, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "صحفي", "Journalist" },
                    { 221, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير اتصالات", "Communications Manager" },
                    { 222, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي توثيق", "Documentation Specialist" },
                    { 223, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "كاتب مقترحات تمويل", "Grant Writer" },
                    { 224, "Content & Creative", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مترجم", "Translator" },
                    { 225, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للتسويق (CMO)", "Chief Marketing Officer (CMO)" },
                    { 226, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للإيرادات (CRO)", "Chief Revenue Officer (CRO)" },
                    { 227, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للمنتجات (CPO)", "Chief Product Officer (CPO)" },
                    { 228, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي لتقنية المعلومات (CIO)", "Chief Information Officer (CIO)" },
                    { 229, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للبيانات (CDO)", "Chief Data Officer (CDO)" },
                    { 230, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي لأمن المعلومات (CSO)", "Chief Security Officer (CSO)" },
                    { 231, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "الرئيس التنفيذي للذكاء الاصطناعي (CAIO)", "Chief AI Officer (CAIO)" },
                    { 232, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير تنفيذي", "Managing Director" },
                    { 233, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير عام", "General Manager" },
                    { 234, "Executive", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "رئيس قسم الهندسة", "Head of Engineering" },
                    { 235, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 4, "مطور Flutter", "Flutter Developer" },
                    { 236, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 4, "مطور React Native", "React Native Developer" },
                    { 237, "Technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 3, "مهندس برمجيات أول", "Senior Software Engineer" },
                    { 238, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "رئيس حسابات", "Chief Accountant" },
                    { 239, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محاسب أول", "Senior Accountant" },
                    { 240, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدقق داخلي", "Internal Auditor" },
                    { 241, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "محلل ائتمان", "Credit Analyst" },
                    { 242, "Finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "رئيس القسم المالي", "Head of Finance" },
                    { 243, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي توظيف تقني", "Technical Recruiter" },
                    { 244, "Human Resources", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي أنظمة موارد بشرية (HRIS)", "HRIS Specialist" },
                    { 245, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مبيعات إقليمي", "Regional Sales Manager" },
                    { 246, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "رئيس قسم المبيعات", "Head of Sales" },
                    { 247, "Sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مستشار ما قبل البيع", "Presales Consultant" },
                    { 248, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير إدارة العمليات", "Operations Director" },
                    { 249, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير مستودع", "Warehouse Manager" },
                    { 250, "Operations", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "مدير أسطول", "Fleet Manager" },
                    { 251, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "رئيس قسم التسويق", "Head of Marketing" },
                    { 252, "Marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 9, "أخصائي PPC", "PPC Specialist" },
                    { 253, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "رئيس قسم التصميم", "Head of Design" },
                    { 254, "Design", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 8, "باحث مستخدمين", "User Researcher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswersV2_AssessmentAttemptId_QuestionId",
                table: "AssessmentAnswersV2",
                columns: new[] { "AssessmentAttemptId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswersV2_QuestionId",
                table: "AssessmentAnswersV2",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAttemptsV2_JobSeekerId_Status_IsActive",
                table: "AssessmentAttemptsV2",
                columns: new[] { "JobSeekerId", "Status", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAttemptsV2_JobTitleId",
                table: "AssessmentAttemptsV2",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestionsV2_SkillId_Difficulty_IsActive",
                table: "AssessmentQuestionsV2",
                columns: new[] { "SkillId", "Difficulty", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_JobSeekerId_IsDeleted",
                table: "Certificates",
                columns: new[] { "JobSeekerId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "City",
                column: "NameEn");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Country",
                column: "NameEn");

            migrationBuilder.CreateIndex(
                name: "IX_Education_JobSeekerId_IsDeleted",
                table: "Educations",
                columns: new[] { "JobSeekerId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_FieldOfStudyId",
                table: "Educations",
                column: "FieldOfStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerifications_UserId",
                table: "EmailVerifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_JobSeekerId_IsDeleted",
                table: "Experiences",
                columns: new[] { "JobSeekerId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CityId",
                table: "Experiences",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CountryId",
                table: "Experiences",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOfStudy_NameEn",
                table: "FieldOfStudy",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CityId",
                table: "Jobs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CountryId",
                table: "Jobs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTitleId",
                table: "Jobs",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RecruiterId",
                table: "Jobs",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CityId",
                table: "JobSeekers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CountryId",
                table: "JobSeekers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_FirstLanguageId",
                table: "JobSeekers",
                column: "FirstLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_JobTitleId",
                table: "JobSeekers",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_SecondLanguageId",
                table: "JobSeekers",
                column: "SecondLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_UserId",
                table: "JobSeekers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_JobSeekerId_SkillId",
                table: "JobSeekerSkills",
                columns: new[] { "JobSeekerId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_SkillId",
                table: "JobSeekerSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId_SkillId",
                table: "JobSkills",
                columns: new[] { "JobId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId",
                table: "JobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitle_TitleEn",
                table: "JobTitle",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_Code",
                table: "Language",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_User_Date",
                table: "Notifications",
                columns: new[] { "UserId", "CreatedAt" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_User_Read_Date",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead", "CreatedAt" },
                descending: new[] { false, false, true });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResets_UserId",
                table: "PasswordResets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileView_Dedup",
                table: "ProfileView",
                columns: new[] { "JobSeekerId", "ViewerRecruiterId", "ViewType", "ViewedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileView_JobSeeker_Date_Type",
                table: "ProfileView",
                columns: new[] { "JobSeekerId", "ViewedAt", "ViewType" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileView_ViewerRecruiterId",
                table: "ProfileView",
                column: "ViewerRecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_JobSeekerId_IsDeleted",
                table: "Projects",
                columns: new[] { "JobSeekerId", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_JobSeeker_Date",
                table: "Recommendations",
                columns: new[] { "JobSeekerId", "GeneratedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_JobId_JobSeekerId",
                table: "Recommendations",
                columns: new[] { "JobId", "JobSeekerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_CityId",
                table: "Recruiters",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_CountryId",
                table: "Recruiters",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_UserId",
                table: "Recruiters",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_JobSeekerId_Unique",
                table: "Resumes",
                column: "JobSeekerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortlistedCandidate_JobId_JobSeekerId",
                table: "ShortlistedCandidate",
                columns: new[] { "JobId", "JobSeekerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortlistedCandidate_JobSeekerId",
                table: "ShortlistedCandidate",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortlistedCandidate_RecruiterId",
                table: "ShortlistedCandidate",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialAccounts_JobSeekerId",
                table: "SocialAccounts",
                column: "JobSeekerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentAnswersV2");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "EmailVerifications");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "JobSeekerSkills");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropTable(
                name: "ProfileView");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "ShortlistedCandidate");

            migrationBuilder.DropTable(
                name: "SocialAccounts");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "AssessmentAttemptsV2");

            migrationBuilder.DropTable(
                name: "AssessmentQuestionsV2");

            migrationBuilder.DropTable(
                name: "FieldOfStudy");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobSeekers");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Recruiters");

            migrationBuilder.DropTable(
                name: "JobTitle");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
