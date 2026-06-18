using System.ComponentModel.DataAnnotations;

namespace RecruitmentPlatformAPI.DTOs.JobSeeker
{
    /// <summary>
    /// Request to update job seeker's job title (subject to 30-day cooldown)
    /// </summary>
    public class UpdateJobTitleDto
    {
        /// <summary>
        /// New job title ID (must be a valid JobTitle from reference data)
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "Job title is required")]
        public int JobTitleId { get; set; }
    }
}
