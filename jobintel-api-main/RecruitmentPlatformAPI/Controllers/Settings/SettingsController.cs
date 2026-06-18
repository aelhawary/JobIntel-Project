using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentPlatformAPI.DTOs.Common;
using RecruitmentPlatformAPI.DTOs.Settings;
using RecruitmentPlatformAPI.Services.Settings;

namespace RecruitmentPlatformAPI.Controllers.Settings
{
    /// <summary>
    /// User notification and display preferences
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ISettingsService settingsService, ILogger<SettingsController> logger)
        {
            _settingsService = settingsService;
            _logger = logger;
        }

        /// <summary>
        /// Get current user's notification and display settings
        /// </summary>
        /// <returns>User settings</returns>
        /// <response code="200">Returns user settings successfully</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<UserSettingsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSettings()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new ApiErrorResponse("Invalid token"));

            var settings = await _settingsService.GetSettingsAsync(userId.Value);
            return Ok(new ApiResponse<UserSettingsDto>(settings));
        }

        /// <summary>
        /// Update current user's notification and display settings (partial update)
        /// </summary>
        /// <param name="dto">Settings to update (only provided fields are updated)</param>
        /// <returns>Updated settings</returns>
        /// <response code="200">Settings updated successfully</response>
        /// <response code="400">Bad request - validation failed</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<UserSettingsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateSettings([FromBody] UpdateSettingsDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new ApiErrorResponse("Invalid token"));

            var settings = await _settingsService.UpdateSettingsAsync(userId.Value, dto);
            return Ok(new ApiResponse<UserSettingsDto>(settings, "Settings updated successfully"));
        }

        private int? GetUserId()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userId, out var id) ? id : null;
        }
    }
}
