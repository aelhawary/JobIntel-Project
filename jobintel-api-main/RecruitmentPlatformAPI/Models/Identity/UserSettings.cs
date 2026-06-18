using System.ComponentModel.DataAnnotations;
using RecruitmentPlatformAPI.Models.Identity;

namespace RecruitmentPlatformAPI.Models.Identity
{
    /// <summary>
    /// User notification and display preferences
    /// </summary>
    public class UserSettings
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Whether the user wants to receive email notifications
        /// </summary>
        public bool EmailNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Whether the user wants to receive weekly digest/summary emails
        /// </summary>
        public bool WeeklyDigestEnabled { get; set; } = true;

        /// <summary>
        /// Whether the recruiter wants to be notified about new candidate matches (Recruiter only)
        /// </summary>
        public bool NewCandidateAlertsEnabled { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User User { get; set; } = null!;
    }
}
