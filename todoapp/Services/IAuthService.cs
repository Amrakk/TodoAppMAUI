using todoapp.Models;

namespace todoapp.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse> LoginAsync(string username, string password);
        Task<ServiceResponse> SignupAsync(string email, string username, string password);
        Task LogoutAsync();
        Task<ServiceResponse> ForgotPasswordAsync(string email);
        Task<ServiceResponse> ResetPasswordAsync(string email, string otp, string newPassword);
        Task<bool> VerifyAsync();
    }
}
