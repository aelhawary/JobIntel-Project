using RecruitmentPlatformAPI.DTOs.Auth;
using RecruitmentPlatformAPI.Models.Identity;

namespace RecruitmentPlatformAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> GoogleAuthAsync(GoogleAuthDto googleAuthDto);
        Task<GoogleUserInfo?> VerifyGoogleTokenAsync(string idToken);
        Task<AuthResponseDto> VerifyEmailAsync(EmailVerificationDto verificationDto);
        Task<AuthResponseDto> ResendVerificationCodeAsync(ResendVerificationDto resendDto);
        Task<AuthResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<AuthResponseDto> ValidateResetTokenAsync(ValidateResetTokenDto validateDto);
        Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<AuthResponseDto> UpdateNameAsync(int userId, UpdateNameDto updateNameDto);
        Task<AuthResponseDto> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        Task<AuthResponseDto> DeactivateAccountAsync(int userId, string authProvider, DeactivateAccountDto dto);
        Task<AuthResponseDto> DeleteAccountAsync(int userId, string authProvider, DeleteAccountDto dto);
    }
}
