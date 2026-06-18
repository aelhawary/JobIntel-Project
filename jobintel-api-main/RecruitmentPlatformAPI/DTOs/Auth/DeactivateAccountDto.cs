using System.ComponentModel.DataAnnotations;

namespace RecruitmentPlatformAPI.DTOs.Auth
{
    /// <summary>
    /// Request to deactivate (soft-delete) the user's account
    /// </summary>
    public class DeactivateAccountDto
    {
        /// <summary>
        /// Current password for verification (required for email-auth users, null for Google-auth users)
        /// </summary>
        /// <example>SecurePass123</example>
        public string? Password { get; set; }

        /// <summary>
        /// Confirmation text that must equal "DEACTIVATE" to proceed
        /// </summary>
        /// <example>DEACTIVATE</example>
        [Required(ErrorMessage = "Confirmation text is required")]
        [RegularExpression("^DEACTIVATE$", ErrorMessage = "Please type DEACTIVATE to confirm")]
        public string Confirmation { get; set; } = string.Empty;
    }
}
