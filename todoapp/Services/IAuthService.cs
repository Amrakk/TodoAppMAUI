using todoapp.Models;

namespace todoapp.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse> LoginAsync(string username, string password);
        Task<ServiceResponse> SignupAsync(string email, string username, string password);
        Task LogoutAsync();
        Task<ServiceResponse> ForgotPassword(string email);
        Task<ServiceResponse> ResetPassword(string email, string otp, string newPassword);
        Task<bool> VerifyAsync();
    }
}
