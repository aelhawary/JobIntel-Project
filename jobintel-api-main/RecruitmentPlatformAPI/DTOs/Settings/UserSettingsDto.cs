namespace RecruitmentPlatformAPI.DTOs.Settings
{
    /// <summary>
    /// User notification and display preferences
    /// </summary>
    public class UserSettingsDto
    {
        /// <summary>
        /// Whether the user wants to receive email notifications
        /// </summary>
        public bool EmailNotificationsEnabled { get; set; }

        /// <summary>
        /// Whether the user wants to receive weekly digest/summary emails
        /// </summary>
        public bool WeeklyDigestEnabled { get; set; }

        /// <summary>
        /// Whether the recruiter wants to be notified about new candidate matches (Recruiter only)
        /// </summary>
        public bool NewCandidateAlertsEnabled { get; set; }
    }

    /// <summary>
    /// Request to update user settings (partial update - only provided fields are updated)
    /// </summary>
    public class UpdateSettingsDto
    {
        /// <summary>
        /// Whether the user wants to receive email notifications
        /// </summary>
        public bool? EmailNotificationsEnabled { get; set; }

        /// <summary>
        /// Whether the user wants to receive weekly digest/summary emails
        /// </summary>
        public bool? WeeklyDigestEnabled { get; set; }

        /// <summary>
        /// Whether the recruiter wants to be notified about new candidate matches (Recruiter only)
        /// </summary>
        public bool? NewCandidateAlertsEnabled { get; set; }
    }
}
