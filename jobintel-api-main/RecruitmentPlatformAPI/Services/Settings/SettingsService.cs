using Microsoft.EntityFrameworkCore;
using RecruitmentPlatformAPI.Data;
using RecruitmentPlatformAPI.DTOs.Settings;
using RecruitmentPlatformAPI.Models.Identity;

namespace RecruitmentPlatformAPI.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(AppDbContext context, ILogger<SettingsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserSettingsDto> GetSettingsAsync(int userId)
        {
            var settings = await _context.UserSettings.FirstOrDefaultAsync(s => s.UserId == userId);

            // Create default settings if none exist (legacy users)
            if (settings == null)
            {
                settings = new UserSettings
                {
                    UserId = userId,
                    EmailNotificationsEnabled = true,
                    WeeklyDigestEnabled = true,
                    NewCandidateAlertsEnabled = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.UserSettings.Add(settings);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created default settings for user {UserId}", userId);
            }

            return new UserSettingsDto
            {
                EmailNotificationsEnabled = settings.EmailNotificationsEnabled,
                WeeklyDigestEnabled = settings.WeeklyDigestEnabled,
                NewCandidateAlertsEnabled = settings.NewCandidateAlertsEnabled
            };
        }

        public async Task<UserSettingsDto> UpdateSettingsAsync(int userId, UpdateSettingsDto dto)
        {
            var settings = await _context.UserSettings.FirstOrDefaultAsync(s => s.UserId == userId);

            if (settings == null)
            {
                settings = new UserSettings
                {
                    UserId = userId,
                    EmailNotificationsEnabled = true,
                    WeeklyDigestEnabled = true,
                    NewCandidateAlertsEnabled = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.UserSettings.Add(settings);
            }

            // Partial update - only update provided fields
            if (dto.EmailNotificationsEnabled.HasValue)
                settings.EmailNotificationsEnabled = dto.EmailNotificationsEnabled.Value;

            if (dto.WeeklyDigestEnabled.HasValue)
                settings.WeeklyDigestEnabled = dto.WeeklyDigestEnabled.Value;

            if (dto.NewCandidateAlertsEnabled.HasValue)
                settings.NewCandidateAlertsEnabled = dto.NewCandidateAlertsEnabled.Value;

            settings.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Settings updated for user {UserId}", userId);

            return new UserSettingsDto
            {
                EmailNotificationsEnabled = settings.EmailNotificationsEnabled,
                WeeklyDigestEnabled = settings.WeeklyDigestEnabled,
                NewCandidateAlertsEnabled = settings.NewCandidateAlertsEnabled
            };
        }
    }
}
