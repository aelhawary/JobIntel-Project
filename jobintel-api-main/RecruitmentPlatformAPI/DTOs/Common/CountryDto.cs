namespace RecruitmentPlatformAPI.DTOs.Common
{
/// <summary>
    /// Country information with localized name
    /// </summary>
    public class CountryDto
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Country name (localized based on lang parameter: en/ar)
        /// </summary>
        /// <example>Egypt</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ISO 3166-1 alpha-2 country code (e.g. "EG", "US", "GB").
        /// Used to render flag icons on the frontend.
        /// </summary>
        /// <example>EG</example>
        public string? CountryCode { get; set; }
    }

    /// <summary>
    /// City information with localized name
    /// </summary>
    public class CityDto
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// City name (localized based on lang parameter)
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
