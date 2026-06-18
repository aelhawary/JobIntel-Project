# JobIntel

> AI-powered recruitment platform connecting job seekers with recruiters through intelligent matching, skill assessments, and streamlined hiring workflows.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-19-61DAFB?logo=react)](https://react.dev/)
[![Python](https://img.shields.io/badge/Python-3.11-3776AB?logo=python)](https://www.python.org/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server)
[![License](https://img.shields.io/badge/License-MIT-yellow)](#license)

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Subprojects](#subprojects)
- [Getting Started](#getting-started)
- [Features](#features)
- [AI Integrations](#ai-integrations)
- [Project Structure](#project-structure)
---

## Overview

JobIntel is a full-stack recruitment platform built as a graduation project at the Egyptian E-Learning University. It automates the hiring pipeline from job posting to candidate assessment using AI-powered tools.

### How It Works

1. **Job Seekers** create profiles, upload resumes (AI-parsed), complete skill assessments, and apply to jobs
2. **Recruiters** post jobs, receive AI-ranked candidate lists, view detailed profiles, and manage applicants
3. **AI Engine** matches candidates to jobs using multi-factor scoring (skills, experience, bio similarity)

---

## Architecture

```
┌─────────────────────────────────────────────────────────────────────┐
│                          CLIENT (Browser)                           │
│              React 19 SPA — Vite 8 — Tailwind CSS 3.4               │
│            Dual dashboards: Job Seeker + Recruiter                  │
└─────────────────────────────┬───────────────────────────────────────┘
                              │ HTTPS (REST)
                              ▼
┌─────────────────────────────────────────────────────────────────────┐
│                       BACKEND API (ASP.NET 9)                       │
│  Controllers (15) → Services (24+) → EF Core → SQL Server (28 tbl)  │
│                                                                     │
│  ┌─────────┐  ┌───────────┐  ┌──────────┐  ┌───────────────────┐    │
│  │  Auth   │  │  Resume   │  │Matching  │  │   Assessment      │    │
│  │Service  │  │  Service  │  │ Service  │  │    Service V2     │    │
│  └────┬────┘  └─────┬─────┘  └────┬─────┘  └────────┬──────────┘    │
└───────┼─────────────┼─────────────┼─────────────────┼───────────────┘
        │             │             │                 │
        ▼             ▼             ▼                 ▼
   Google OAuth   Google Gemini  HuggingFace      Groq Cloud
                  (CV Parsing)    (Matching)    (Assessment Gen)
```

---

## Tech Stack

### Backend

| Component | Technology | Version |
|-----------|-----------|---------|
| Framework | ASP.NET Core | 9.0 |
| Language | C# | 13 |
| ORM | Entity Framework Core | 9.0.10 |
| Database | SQL Server | 2022 |
| Auth | JWT Bearer + Google OAuth | — |
| PDF Parsing | PdfPig + OpenXml | 0.1.14 / 3.5.1 |
| Email | Brevo API + SMTP | — |
| Docs | Swashbuckle (Swagger) | 6.5.0 |

### Frontend

| Component | Technology | Version |
|-----------|-----------|---------|
| UI Library | React | 19.1.1 |
| Build Tool | Vite | 8.0.16 |
| Styling | Tailwind CSS | 3.4 |
| Routing | React Router DOM | 7.9.4 |
| i18n | i18next | 26.3.1 |
| Charts | Recharts | 3.3.0 |
| Auth | Google OAuth | 0.13.4 |

### AI Services

| Service | Provider | Model |
|---------|----------|-------|
| Candidate Matching | HuggingFace Spaces | Custom ML |
| CV Parsing | Google Gemini | gemini-2.5-flash |
| Assessment Generation | Groq Cloud | llama-3.3-70b-versatile |

---

## Subprojects

| Directory | Description | README |
|-----------|-------------|--------|
| `jobintel-api-main/` | ASP.NET Core 9 backend API | [Backend README](jobintel-api-main/README.md) |
| `jobintel-frontend/` | React 19 SPA frontend | [Frontend README](JobIntel-Frontend-Project/README.md) |
| `AI/` | AI matching engine (HuggingFace Spaces) | [AI README](AI/README.md) |

---

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB, Express, or full instance)
- [Node.js](https://nodejs.org/) v18+
- [Google Cloud Console](https://console.cloud.google.com/) project (for Gemini API key)
- [Groq Cloud](https://console.groq.com/) account (for assessment generation)
- [Brevo](https://app.brevo.com/) account (for transactional emails)

### 1. Backend Setup

```bash
cd jobintel-api-main/RecruitmentPlatformAPI

# Restore dependencies
dotnet restore

# Configure API keys
cp appsettings.example.json appsettings.Development.json
# Edit appsettings.Development.json with your connection string and API keys

# Apply database migrations
dotnet ef database update

# Run the API
dotnet run
```

Swagger UI: `http://localhost:5217/swagger`

### 2. Frontend Setup

```bash
cd JobIntel-Frontend-Project

# Install dependencies
npm install

# Start dev server
npm run dev
```

Frontend: `http://localhost:5173`

### 3. AI Engine

The AI matching engine is hosted externally on HuggingFace Spaces. No local setup required.

- **URL:** `https://alikhaled123-ai-recruitment-api.hf.space/api/recommend`
- **Status:** Auto-sleeps after 48h inactivity (free tier)

---

## Features

### For Job Seekers

- Multi-step profile wizard with resume upload and AI parsing
- AI-powered skill assessment (20 MCQ questions, timed, anti-cheat)
- Real-time notifications
- Bilingual support (English/Arabic)
- Dark mode

### For Recruiters

- Job posting and management
- AI-ranked candidate matching
- Candidate profile viewing with shortlisting
- Resume download and direct contact
- Company profile management
- Dashboard with analytics

### Technical Features

- JWT authentication with Google OAuth
- Role-based access control (JobSeeker / Recruiter)
- Profile completion enforcement
- 30-second notification polling
- Lazy-loaded dashboard pages
- Responsive design with mobile sidebar drawers
- Full i18n with RTL layout support

---

## AI Integrations

### 1. Candidate Matching

- Three-factor scoring: Skills (50%) + Experience (35%) + Bio similarity (15%)
- Results cached for 5 minutes
- Fairness penalty for unassessed candidates (-20%)
- Graceful degradation when AI service is unavailable

### 2. CV Parsing

- Upload PDF/DOCX → Gemini extracts structured JSON
- Fields: personal info, experience, education, skills, projects, certificates
- Fallback regex extraction if JSON parsing fails
- Circuit breaker: 3 failures → 60s cooldown

### 3. Assessment Generation

- Dynamic MCQ generation based on job title + seniority
- 16 technical + 4 soft skill questions per assessment
- Multi-layer validation and deduplication
- Anti-cheat: tab-switch detection, timer expiry

---

## Project Structure

```
Project/
├── jobintel-api-main/              # Backend API
│   ├── RecruitmentPlatformAPI/     # ASP.NET project
│   │   ├── Controllers/            # 15 API controllers
│   │   ├── Services/               # 24+ business logic services
│   │   ├── Models/                 # 28 entity models
│   │   ├── DTOs/                   # Data Transfer Objects
│   │   ├── Data/                   # EF Core + migrations + seeds
│   │   ├── Enums/                  # 13 enum types
│   │   └── Program.cs              # Entry point
│   └── README.md
│
├── JobIntel-Frontend-Project/      # Frontend SPA
│   ├── src/
│   │   ├── api/                    # 9 API service modules
│   │   ├── context/                # 3 React Contexts
│   │   ├── Components/             # 22 shared components
│   │   ├── pages/                  # 40 page components
│   │   ├── layout/                 # Employee + HR layouts
│   │   ├── routes/                 # 5 route guards
│   │   ├── hooks/                  # Custom hooks
│   │   ├── theme/                  # Light/dark mode
│   │   └── lib/                    # i18n, utils, alerts
│   └── README.md
│
├── AI/                             # AI Matching Engine
│   ├── ai_engine.py                # Matching algorithm
│   ├── main.py                     # API entry point
│   └── README.md
│
└── README.md                       # ← You are here
```

