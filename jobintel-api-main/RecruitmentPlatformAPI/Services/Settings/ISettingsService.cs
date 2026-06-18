using RecruitmentPlatformAPI.DTOs.Settings;

namespace RecruitmentPlatformAPI.Services.Settings
{
    public interface ISettingsService
    {
        Task<UserSettingsDto> GetSettingsAsync(int userId);
        Task<UserSettingsDto> UpdateSettingsAsync(int userId, UpdateSettingsDto dto);
    }
}
