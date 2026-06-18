# JobIntel — Backend API

> ASP.NET Core 9.0 REST API powering the JobIntel AI-powered recruitment platform.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server)
[![License](https://img.shields.io/badge/License-MIT-yellow)](../)

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Database](#database)
- [Authentication](#authentication)
- [AI Integrations](#ai-integrations)
- [Configuration](#configuration)
- [Security](#security)

---

## Overview

The JobIntel backend is a monolithic ASP.NET Core 9.0 Web API implementing the **Controller-Service pattern** with strict DTOs. It provides RESTful endpoints for authentication, profile management, job postings, AI-powered candidate matching, dynamic skill assessments, and engagement analytics.

### Key Numbers

| Metric | Value |
|--------|-------|
| Controllers | 15 |
| Services | 24+ |
| Database Tables | 28 |
| API Endpoints | 89+ |
| Entity Models | 28 |
| Enum Types | 13 |

---

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                      PRESENTATION LAYER                     │
│           Controllers (15) — HTTP routing + validation      │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│                    BUSINESS LOGIC LAYER                     │
│              Services (24+) — Core logic + AI calls         │
│    AuthService | ResumeService | AIMatchingService | ...    │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│                      DATA ACCESS LAYER                      │
│      Entity Framework Core — AppDbContext (28 DbSets)       │
└──────────────────────────┬──────────────────────────────────┘
                           │
              ┌────────────▼────────────┐
              │      SQL Server         │
              │     (28 tables)         │
              └─────────────────────────┘
```

---

## Tech Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| Framework | ASP.NET Core | 9.0 |
| Language | C# | 13 |
| ORM | Entity Framework Core | 9.0.10 |
| Database | SQL Server | 2022 |
| Authentication | JWT Bearer | 9.0.10 |
| Password Hashing | BCrypt.Net-Next | 4.0.3 |
| Email | Brevo HTTP API + SMTP fallback | — |
| Google OAuth | Google.Apis.Auth | 1.73.0 |
| PDF Parsing | PdfPig | 0.1.14 |
| DOCX Parsing | DocumentFormat.OpenXml | 3.5.1 |
| API Docs | Swashbuckle (Swagger) | 6.5.0 |
| AI - CV Parsing | Google Gemini API | gemini-2.5-flash |
| AI - Matching | External Python API | HuggingFace Spaces |
| AI - Assessments | Groq Cloud API | llama-3.3-70b-versatile |

---

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB, Express, or full instance)
- [Google Cloud Console](https://console.cloud.google.com/) project (for Gemini API key)
- [Groq Cloud](https://console.groq.com/) account (for assessment generation API key)
- [Brevo](https://app.brevo.com/) account (for transactional emails)

---

## Getting Started

```bash
# Clone the repository
git clone https://github.com/your-org/jobintel.git
cd jobintel/backend/RecruitmentPlatformAPI

# Restore dependencies
dotnet restore

# Create development configuration
cp appsettings.example.json appsettings.Development.json
# Edit appsettings.Development.json with your API keys and connection string

# Apply database migrations and seed data
dotnet ef database update

# Run the application
dotnet run
```

Swagger UI is available at `http://localhost:5217/swagger` in development mode.

### Environment Variables

All sensitive configuration lives in `appsettings.Development.json` (gitignored) or production environment variables. Never commit API keys or secrets.

---

## Project Structure

```
RecruitmentPlatformAPI/
├── Program.cs                          # Entry point, DI registration, middleware pipeline
├── AppDbContextFactory.cs              # Design-time DbContext factory for EF migrations
├── appsettings.json                    # Base configuration
├── appsettings.Development.json        # Dev overrides (gitignored)
├── appsettings.Production.json         # Production overrides
│
├── Configuration/                      # Strongly-typed settings POCOs
│   ├── JwtSettings.cs
│   ├── EmailSettings.cs
│   ├── FileStorageSettings.cs
│   ├── LlmSettings.cs
│   └── AssessmentSettings.cs
│
├── Controllers/                        # 15 API controllers
│   ├── Common/
│   │   ├── BaseApiController.cs        # Abstract base with GetCurrentUserId()
│   │   ├── CountriesController.cs
│   │   ├── LanguagesController.cs
│   │   └── FieldsOfStudyController.cs
│   ├── Auth/AuthController.cs
│   ├── JobSeeker/
│   │   ├── JobSeekerController.cs
│   │   ├── ResumeController.cs
│   │   ├── ExperienceController.cs
│   │   ├── EducationController.cs
│   │   ├── ProjectsController.cs
│   │   ├── CertificatesController.cs
│   │   ├── JobSeekerSkillsController.cs
│   │   └── SocialAccountsController.cs
│   ├── Recruiter/
│   │   ├── RecruiterController.cs
│   │   ├── JobsController.cs
│   │   └── RecruiterCandidatesController.cs
│   ├── Assessment/AssessmentV2Controller.cs
│   ├── Notification/NotificationController.cs
│   └── Settings/SettingsController.cs
│
├── Services/                           # 24+ business logic services
│   ├── Auth/                           # AuthService, TokenService, EmailService
│   ├── JobSeeker/                      # Profile, Resume, Experience, Education, Skills...
│   ├── Recruiter/                      # RecruiterService, JobService, AIMatchingService
│   ├── Assessment/V2/                  # AssessmentServiceV2, GroqQuestionGenerator
│   ├── Notification/                   # NotificationService
│   ├── Settings/                       # SettingsService
│   └── Shared/                         # SkillMatcher, FuzzyMatchHelper, WeeklyDigestService
│
├── Models/                             # 28 entity classes
│   ├── Identity/                       # User, UserSettings, EmailVerification, PasswordReset
│   ├── JobSeeker/                      # JobSeeker, Experience, Education, Project, Resume...
│   ├── Recruiter/                      # Recruiter, ShortlistedCandidate
│   ├── Jobs/                           # Job, JobSkill, Recommendation
│   ├── Assessment/V2/                  # AssessmentAttemptV2, AssessmentQuestionV2, AssessmentAnswerV2
│   ├── Notification/                   # Notification
│   └── Reference/                      # Skill, JobTitle, Country, City, Language, FieldOfStudy
│
├── DTOs/                               # Data Transfer Objects (30+ files)
│   ├── Auth/                           # RegisterDto, LoginDto, GoogleAuthDto...
│   ├── Common/                         # ApiResponse<T>, CountryDto, WizardDtos...
│   ├── JobSeeker/                      # ProfileDtos, SkillDtos, ResumeDtos...
│   ├── Recruiter/                      # RecruiterDtos, JobDtos, AIMatchingDtos...
│   ├── Assessment/V2/                  # AssessmentV2Dtos
│   └── Notification/                   # NotificationDto
│
├── Data/
│   ├── AppDbContext.cs                 # DbContext with Fluent API (567 lines)
│   ├── Migrations/                     # EF Core migration files
│   ├── Seed/                           # Seeder classes + JSON data files
│   └── SeedData/                       # countries.json, skills.json, job-titles.json...
│
├── Enums/                              # 13 enum types
│   ├── AccountType.cs                  # JobSeeker, Recruiter
│   ├── AuthProvider.cs                 # Email, Google
│   ├── AuthErrorCode.cs
│   ├── AssessmentStatus.cs
│   ├── Degree.cs
│   ├── EmploymentType.cs
│   ├── ExperienceSeniorityLevel.cs
│   ├── JobTitleRoleFamily.cs           # Backend, Frontend, FullStack, Data, Mobile...
│   ├── LanguageProficiency.cs
│   ├── NotificationType.cs
│   ├── QuestionCategory.cs
│   ├── QuestionDifficulty.cs
│   └── WorkModel.cs                    # Remote, Hybrid, OnSite
│
├── Uploads/                            # Runtime file storage (gitignored content)
│   ├── Resumes/
│   └── ProfilePictures/
│
└── wwwroot/                            # Static files (default profile picture)
```

---

## API Endpoints

### Authentication (`/api/auth/`) — 15 endpoints

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/register` | No | Register (JobSeeker or Recruiter) |
| POST | `/login` | No | Login with email/password |
| POST | `/google` | No | Login/register with Google OAuth |
| POST | `/verify-email` | No | Verify email with 6-digit code |
| POST | `/resend-verification` | No | Resend verification code |
| POST | `/forgot-password` | No | Request password reset link |
| POST | `/validate-reset-token` | No | Check if reset token is valid |
| POST | `/reset-password` | No | Reset password with token |
| GET | `/me` | Yes | Get current user from JWT |
| PUT | `/name` | Yes | Update first/last name |
| POST | `/change-password` | Yes | Change password (old + new) |
| POST | `/deactivate` | Yes | Soft-deactivate account |
| POST | `/delete-account` | Yes | Permanently delete account |

### Job Seeker Profile (`/api/jobseeker/`) — 35+ endpoints

| Group | Endpoints | Description |
|-------|-----------|-------------|
| Profile | 4 | Get/update profile, wizard status, advance wizard |
| Profile Picture | 5 | Upload, get info, download, delete, exists |
| Resume | 6 | Upload, parse (AI), get, download, delete, check exists |
| Experience | 4 | CRUD with soft delete |
| Education | 4 | CRUD with soft delete |
| Projects | 4 | CRUD with soft delete |
| Certificates | 4 | CRUD with soft delete |
| Skills | 3 | Add/remove claimed skills |
| Social Links | 3 | Get/update social accounts |

### Recruiter (`/api/recruiter/`) — 15+ endpoints

| Group | Endpoints | Description |
|-------|-----------|-------------|
| Profile | 3 | Get/update recruiter profile, company info |
| Jobs | 8 | Full CRUD with activate/deactivate |
| Candidates | 7 | AI-matched candidates, shortlist, view, contact, resume download |

### Assessments V2 (`/api/assessment/v2/`) — 12 endpoints

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/eligibility` | Yes | Check if user can start assessment |
| POST | `/start` | Yes | Start new 20-question assessment |
| POST | `/resume` | Yes | Resume in-progress assessment |
| GET | `/current` | Yes | Get in-progress assessment status |
| GET | `/questions` | Yes | Get answered/unanswered status |
| GET | `/question` | Yes | Get next unanswered question |
| GET | `/question/{n}` | Yes | Get question by 1-based number |
| POST | `/answer` | Yes | Submit/overwrite answer |
| POST | `/complete` | Yes | Finalize and compute scores |
| POST | `/abandon` | Yes | Abandon assessment (no cooldown) |
| GET | `/history` | Yes | All previous attempts |
| GET | `/result/{id}` | Yes | Full review with correct answers |

### Notifications (`/api/notifications/`) — 3 endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/` | List notifications (paginated) |
| POST | `/{id}/read` | Mark as read |
| POST | `/mark-all-read` | Mark all as read |

### Reference Data (`/api/`) — 5+ endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/countries?lang=en\|ar` | Countries (bilingual) |
| GET | `/countries/{id}/cities` | Cities by country (bilingual) |
| GET | `/languages?lang=en\|ar` | Languages (bilingual) |
| GET | `/skills` | Skills list |
| GET | `/job-titles` | Job titles list |
| GET | `/fields-of-study` | Fields of study |

---

## Database

**28 tables** across 7 categories with EF Core code-first approach:

| Category | Tables |
|----------|--------|
| Identity | User, UserSettings, EmailVerification, PasswordReset |
| Job Seeker | JobSeeker, Experience, Education, Project, Certificate, Resume, SocialAccount, JobSeekerSkill, ProfileView |
| Recruiter | Recruiter, ShortlistedCandidate |
| Jobs | Job, JobSkill, Recommendation |
| Assessment V2 | AssessmentAttemptV2, AssessmentQuestionV2, AssessmentAnswerV2 |
| Notification | Notification |
| Reference | Country, City, Language, JobTitle, Skill, FieldOfStudy |

### Seed Data

- **90** job titles across 8 role families
- **~5,000** cities across **~250** countries
- **50** languages with ISO codes
- **~300** technical and soft skills with aliases
- **~100** fields of study

All reference data is bilingual (NameEn + NameAr) for full English/Arabic support.

---

## Authentication

### JWT Bearer Tokens

- Algorithm: HMAC-SHA256
- Expiration: 24 hours
- Claims: UserId, Email, FullName, Role, FirstName, LastName, ProfileCompletionStep

### Google OAuth

1. Frontend obtains Google ID token
2. Backend validates with Google's public keys
3. Auto-creates account for new users
4. Returns JWT token

### Security Features

- BCrypt password hashing (work factor 12)
- Account lockout after 5 failed attempts (15-minute cooldown)
- Email verification required before first login
- Forgot password always returns 200 OK (prevents email enumeration)
- Role-based authorization (`[Authorize(Roles = "JobSeeker")]`)

---

## AI Integrations

### 1. CV Parsing — Google Gemini

- Model: `gemini-2.5-flash`
- Extracts structured data from PDF/DOCX resumes
- Circuit breaker: 3 failures → 60-second cooldown
- Post-LLM skill validation to prevent hallucination

### 2. Candidate Matching — External AI Engine

- Hosted on HuggingFace Spaces
- Three-factor scoring: Skill overlap (50%), Experience proximity (35%), Bio proximity (15%)
- Results cached in `IMemoryCache` (5-minute TTL)
- Fallback to `Recommendations` table when AI is unavailable
- 20% fairness penalty for unassessed candidates

### 3. Assessment Generation — Groq Cloud

- Model: `llama-3.3-70b-versatile`
- Generates 20 MCQ questions (16 technical + 4 soft skill)
- Dynamic difficulty calibration by seniority level
- Multi-layer validation and deduplication

---

## Configuration

| Setting | File | Description |
|---------|------|-------------|
| `ConnectionStrings` | appsettings.*.json | SQL Server connection string |
| `JwtSettings` | appsettings.*.json | JWT secret, issuer, audience, expiry |
| `EmailSettings` | appsettings.*.json | SMTP/Brevo configuration |
| `LlmSettings` | appsettings.*.json | Groq and Gemini API keys |
| `FileStorageSettings` | appsettings.*.json | Upload paths, file size limits |

---

## Security

- BCrypt password hashing with salt
- JWT Bearer tokens with HMAC-SHA256 signing
- Account lockout after failed attempts
- Email verification required before login
- Role-based authorization (JobSeeker / Recruiter)
- CORS policy restricted to allowed origins
- File upload validation (type, size, MIME)
- Parameterized queries via EF Core (SQL injection protection)
- React XSS escaping on frontend