# JobIntel — Frontend

> React 19 single-page application for the JobIntel AI-powered recruitment platform.

[![React](https://img.shields.io/badge/React-19-61DAFB?logo=react)](https://react.dev/)
[![Vite](https://img.shields.io/badge/Vite-8-646CFF?logo=vite)](https://vite.dev/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-3.4-06B6D4?logo=tailwindcss)](https://tailwindcss.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow)](../)

## Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Routing](#routing)
- [Architecture](#architecture)
- [Key Features](#key-features)
- [Configuration](#configuration)

---

## Overview

JobIntel Frontend is a modern SPA built with React 19 and Vite 8. It serves two roles — **Job Seeker** and **Recruiter** — with role-specific dashboards, multi-step profile wizards, AI-powered assessments, and full English/Arabic bilingual support with RTL layout.

### Key Numbers

| Metric | Value |
|--------|-------|
| Page Components | 40 |
| Shared Components | 22 |
| API Service Modules | 9 |
| Context Providers | 5 |
| Route Guards | 5 |
| Locales | 2 (English, Arabic) |
| Source Files | 105 |

---

## Tech Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| UI Library | React | 19.1.1 |
| Build Tool | Vite | 8.0.16 |
| Styling | Tailwind CSS | 3.4 |
| Routing | React Router DOM | 7.9.4 |
| HTTP Client | Axios | 1.13.5 |
| i18n | i18next + react-i18next | 26.3.1 / 17.0.8 |
| Charts | Recharts | 3.3.0 |
| Icons | Lucide React, FontAwesome | 0.546 / 7.1 |
| Auth | Google OAuth (`@react-oauth/google`) | 0.13.4 |
| Alerts | SweetAlert2 | 11.26.3 |
| Markdown | React Markdown | 10.1.0 |

---

## Prerequisites

- [Node.js](https://nodejs.org/) v18+ (v20+ recommended)
- [npm](https://www.npmjs.com/) v9+
- Backend API running at `https://jobintel-app.runasp.net/api` (or your own instance)

---

## Getting Started

```bash
# Clone the repository
git clone https://github.com/your-org/jobintel.git
cd jobintel/frontend

# Install dependencies
npm install

# Start development server
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview
```

The dev server runs at `http://localhost:5173` by default.

### Available Scripts

| Script | Description |
|--------|-------------|
| `npm run dev` | Start Vite dev server with HMR |
| `npm run build` | Production build to `dist/` |
| `npm run preview` | Preview production build locally |
| `npm run lint` | Run ESLint |

---

## Project Structure

```
src/
├── main.jsx                           # Entry point, provider nesting
├── App.jsx                            # Route definitions + Suspense
│
├── api/                               # API service layer (9 modules)
│   ├── axios.js                       # Axios instance, JWT interceptor, 401 handling
│   ├── authService.js                 # Register, login, Google OAuth, password reset
│   ├── wizardService.js               # Profile wizard CRUD + reference data caching
│   ├── jobsService.js                 # Job CRUD, skill search
│   ├── assessmentService.js           # Assessment lifecycle (start, answer, complete)
│   ├── recruiterCandidateService.js   # Candidate matching, shortlisting, contact
│   ├── recruiterDashboardService.js   # HR dashboard aggregation, company info
│   ├── notificationService.js         # Notifications, unread count
│   ├── settingsService.js             # Account settings, password, deactivation
│   └── engagementService.js           # Job seeker engagement stats
│
├── context/                           # React Context providers
│   ├── AuthContext.jsx                # JWT auth, localStorage, role mapping
│   ├── PictureContext.jsx             # Profile picture state + cache-busting
│   └── NotificationContext.jsx        # Polling (30s), unread count, mark read
│
├── hooks/                             # Custom hooks
│   ├── useConfirmLogout.js            # SweetAlert2 logout confirmation
│   └── useSessionStorage.js           # sessionStorage-backed state
│
├── Components/                        # Shared components (22 files)
│   ├── Navbar.jsx                     # Public navigation
│   ├── Modal.jsx                      # Reusable modal
│   ├── SearchableSelect.jsx           # Filterable dropdown
│   ├── ThemeToggle.jsx                # Light/dark/system toggle
│   ├── ElectricBorder.jsx             # Animated glow effect
│   ├── ProfileSidebarStepper.jsx      # Profile wizard progress
│   ├── ProfileSetupLayout.jsx         # Wizard layout wrapper
│   ├── TrendBarChart.jsx              # Recharts bar chart wrapper
│   └── ui/                            # Primitive UI components
│       ├── Card.jsx
│       ├── EmptyState.jsx
│       ├── ExpandableText.jsx
│       ├── Pill.jsx
│       ├── SectionCard.jsx
│       ├── TabBar.jsx
│       └── Timeline.jsx
│
├── layout/                            # Layout shells
│   ├── employee/
│   │   ├── EmployeeLayout.jsx         # Sidebar + Outlet
│   │   └── Sidebar.jsx               # Employee navigation sidebar
│   └── hr/
│       ├── HRLayout.jsx               # Sidebar + Outlet
│       └── HRSidebar.jsx             # HR navigation sidebar
│
├── pages/                             # Page components (40 files)
│   ├── Home.jsx, About.jsx, Login.jsx, Signup.jsx, NotFound.jsx
│   ├── auth/                          # VerifyEmail, ForgotPassword, ResetPassword
│   ├── step-profile/                  # Job seeker onboarding (4 steps)
│   ├── recruiter/                     # RecruiterSetup wizard
│   ├── employee/                      # Job seeker dashboard (7 pages)
│   │   └── assessment/                # Assessment subsystem (5 components, hooks, utils)
│   └── hr/                            # Recruiter dashboard (6 pages)
│
├── routes/                            # Route guards (5 files)
│   ├── PrivateRoute.jsx               # Auth required (Outlet)
│   ├── RequireAuth.jsx                # Auth required (children)
│   ├── RoleRoute.jsx                  # Role check
│   ├── RequireWizardStep.jsx          # Job seeker step guard
│   └── RequireRecruiterWizardStep.jsx # Recruiter step guard
│
├── loader/                            # Global loading overlay
│   └── LoaderProvider.jsx
│
├── theme/                             # Theming system
│   ├── ThemeProvider.jsx              # Light/dark/system, Tailwind class
│   └── theme.css                      # CSS variables for themes
│
├── styles/                            # Global CSS
│   ├── globals.css                    # Tailwind directives, RTL, animations
│   ├── FullScreenLoader.css
│   └── SciFiScanner.css
│
├── lib/                               # Utilities
│   ├── i18n.js                        # i18next setup + RTL toggle
│   ├── alerts.js                      # SweetAlert2 wrappers
│   ├── utils.js                       # cn() class merge helper
│   └── locales/                       # en.json (61KB), ar.json (78KB)
│
└── utils/
    └── extractError.js                # ASP.NET error extractor with i18n
```

---

## Routing

### Public Routes (7)

| Path | Component | Description |
|------|-----------|-------------|
| `/` | Home | Landing page |
| `/about` | About | About page |
| `/login` | Login | Email/password login |
| `/signup` | Signup | Registration |
| `/verify-email` | VerifyEmail | Email verification |
| `/forgot-password` | ForgotPassword | Password reset request |
| `/reset-password` | ResetPassword | Password reset form |

### Job Seeker Routes (8)

| Path | Component | Guard |
|------|-----------|-------|
| `/assessment-test` | AssessmentTest | RequireAuth |
| `/employee` | Dashboard | RequireAuth + RoleRoute |
| `/employee/profile` | Profile | RequireAuth + RoleRoute |
| `/employee/tests` | Assessment | RequireAuth + RoleRoute |
| `/employee/interview` | VideoInterview | RequireAuth + RoleRoute |
| `/employee/jobs` | Jobs | RequireAuth + RoleRoute |
| `/employee/notifications` | Notifications | RequireAuth + RoleRoute |
| `/employee/settings` | Settings | RequireAuth + RoleRoute |

### Recruiter Routes (6)

| Path | Component | Guard |
|------|-----------|-------|
| `/hr` | HRDashboard | RequireAuth + RoleRoute |
| `/hr/jobs` | HRJobs | RequireAuth + RoleRoute |
| `/hr/candidates` | HRCandidates | RequireAuth + RoleRoute |
| `/hr/candidates/:candidateId` | HRCandidateProfile | RequireAuth + RoleRoute |
| `/hr/notifications` | HRNotifications | RequireAuth + RoleRoute |
| `/hr/settings` | HRSettings | RequireAuth + RoleRoute |

### Wizard Routes (6)

| Path | Component | Guard |
|------|-----------|-------|
| `/step-0` through `/step-3` | Profile wizard steps | RequireAuth + RequireWizardStep |
| `/recruiter-setup` | RecruiterSetup | RequireAuth + RequireRecruiterWizardStep |

---

## Architecture

### Provider Nesting

```
BrowserRouter
└── GoogleOAuthProvider
    └── AuthProvider              ← JWT auth, role mapping
        └── PictureProvider       ← Profile picture state
            └── NotificationProvider  ← 30s polling
                └── LoaderProvider    ← Global loading overlay
                    └── ThemeProvider     ← Light/dark/system
                        └── App
```

### Role System

| API Role | Frontend Role | Layout |
|----------|---------------|--------|
| JobSeeker | employee | EmployeeLayout (sidebar + outlet) |
| Recruiter | hr | HRLayout (sidebar + outlet) |

### Lazy Loading

Dashboard pages (employee + HR) and layouts are `React.lazy()` loaded with `<Suspense fallback={<FullScreenLoader />}>`. Public pages are statically imported for fast initial load.

---

## Key Features

- **Dual Role System** — Separate dashboards for job seekers and recruiters
- **Profile Completion Wizard** — Multi-step onboarding with step guards preventing skipping
- **AI-Powered Assessment** — Timed MCQ exams with anti-cheat (tab-switch detection), retry logic, and result review
- **Video Interview** — Browser-based recording via MediaRecorder API
- **Bilingual (EN/AR)** — Full i18next integration with RTL layout support
- **Dark Mode** — Light/dark/system theme with CSS variables and Tailwind `dark` class
- **Real-time Notifications** — 30-second polling interval for unread count
- **Google OAuth** — Social login integration
- **Responsive Design** — Mobile-friendly sidebars with drawer + focus trap
- **Error Handling** — Centralized ASP.NET error extraction with i18n translation

---

## Configuration

### Backend API URL

The API base URL is configured in `src/api/axios.js`:

```javascript
baseURL: "https://jobintel-app.runasp.net/api"
```

Change this to your backend URL before building.

### Google OAuth

The Google Client ID is passed via `main.jsx` to `GoogleOAuthProvider`. Obtain yours from [Google Cloud Console](https://console.cloud.google.com/).

### Environment Variables

This project does not use `.env` files. All configuration is in source code for simplicity. For production, update the values in `src/api/axios.js` and `src/main.jsx`.
