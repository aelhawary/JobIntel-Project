using System.ComponentModel.DataAnnotations;

namespace RecruitmentPlatformAPI.DTOs.Auth
{
    /// <summary>
    /// Request to permanently delete the user's account and all associated data
    /// </summary>
    public class DeleteAccountDto
    {
        /// <summary>
        /// Current password for verification (required for email-auth users, null for Google-auth users)
        /// </summary>
        /// <example>SecurePass123</example>
        public string? Password { get; set; }

        /// <summary>
        /// Confirmation text that must equal "DELETE" to proceed
        /// </summary>
        /// <example>DELETE</example>
        [Required(ErrorMessage = "Confirmation text is required")]
        [RegularExpression("^DELETE$", ErrorMessage = "Please type DELETE to confirm")]
        public string Confirmation { get; set; } = string.Empty;
    }
}
