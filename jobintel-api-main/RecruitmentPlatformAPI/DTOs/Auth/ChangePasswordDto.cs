using System.ComponentModel.DataAnnotations;

namespace RecruitmentPlatformAPI.DTOs.Auth
{
    /// <summary>
    /// Request to change password for an authenticated user
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// Current password for verification
        /// </summary>
        /// <example>OldSecurePass123</example>
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// New password (minimum 8 characters, must contain uppercase, lowercase, and digit)
        /// </summary>
        /// <example>NewSecurePass123</example>
        [Required(ErrorMessage = "New password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit")]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Confirm new password (must match new password)
        /// </summary>
        /// <example>NewSecurePass123</example>
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
