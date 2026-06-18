using System.ComponentModel.DataAnnotations;

namespace RecruitmentPlatformAPI.DTOs.Recruiter
{
    /// <summary>
    /// Request to update recruiter's company name (subject to 30-day cooldown)
    /// </summary>
    public class UpdateCompanyNameDto
    {
        /// <summary>
        /// New company name
        /// </summary>
        /// <example>Acme Corp</example>
        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(150, ErrorMessage = "Company name cannot exceed 150 characters")]
        public string CompanyName { get; set; } = string.Empty;
    }
}
