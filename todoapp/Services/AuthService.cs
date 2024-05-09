using System.Net;
using System.Text;
using System.Text.Json;
using todoapp.Models;

namespace todoapp.Services
{
    public class AuthService : IAuthService
    {
        HttpClient _client;

        public AuthService(RestService service)
        {
            _client = service.GetClient;
        }

        public async Task<ServiceResponse> LoginAsync(string username, string password)
        {
            try { 
                if(Connectivity.NetworkAccess != NetworkAccess.Internet) 
                    return new ServiceResponse()
                        { Status = Status.Error, Message = "No internet connection" };

                HttpContent content = new StringContent(
                    JsonSerializer.Serialize(new { username, password }), 
                    Encoding.UTF8, 
                    "application/json"
                );
                HttpResponseMessage res = await _client.PostAsync("login", content);
                if (res.IsSuccessStatusCode)
                {
                    RestService.SaveCookies(res);
                    return new ServiceResponse()
                    { Status = Status.Success, Message = "Login successful" };
                }
                else if (res.StatusCode == HttpStatusCode.Forbidden)
                    return new ServiceResponse()
                    { Status = Status.Failed, Message = "Account not activated" };
                else if (res.StatusCode == HttpStatusCode.Unauthorized)
                    return new ServiceResponse()
                    { Status = Status.Failed, Message = "Invalid username or password" };
                else
                    return new ServiceResponse()
                    { Status = Status.Error, Message = "Internal Server Error" };

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new ServiceResponse()
                    { Status = Status.Error, Message = "Internal Client Error" };
            }
        }

        public async Task<ServiceResponse> SignupAsync(string email, string username, string password)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    return new ServiceResponse()
                    { Status = Status.Error, Message = "No internet connection" };

                HttpContent content = new StringContent(
                    JsonSerializer.Serialize(new { email, username, password }),
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage res = await _client.PostAsync("signup", content);
                if (res.IsSuccessStatusCode)
                {
                    return new ServiceResponse()
                    { Status = Status.Success, Message = "Account Created" };
                }
                else if (res.StatusCode == HttpStatusCode.Conflict)
                {
                    var message = await res.Content.ReadAsStringAsync();
                    message = JsonSerializer.Deserialize<APIResponse>(json: message)!.message;
                    return new ServiceResponse()
                    { Status = Status.Failed, Message = message };
                }
                else if (res.StatusCode == HttpStatusCode.Unauthorized)
                    return new ServiceResponse()
                    { Status = Status.Failed, Message = "Invalid username or password" };
                else
                    return new ServiceResponse()
                    { Status = Status.Error, Message = "Internal Server Error" };
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ServiceResponse()
                { Status = Status.Error, Message = "Internal Client Error" };
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _client.PostAsync("logout", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        public async Task<ServiceResponse> ResetPasswordAsync(string email, string otp, string newPassword)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "reset-password");
            request.Content = new StringContent(JsonSerializer.Serialize(new { email, otp, password = newPassword }), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage res = await _client.SendAsync(request);
                if (res.IsSuccessStatusCode)
                {
                    return new ServiceResponse()
                    { Status = Status.Success, Message = "Password reset successful" };
                }
                else if (res.StatusCode == HttpStatusCode.Forbidden)
                {
                    return new ServiceResponse()
                    { Status = Status.Failed, Message = "Invalid OTP" };
                }
                else
                {
                    return new ServiceResponse()
                    { Status = Status.Error, Message = "Internal Server Error" };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ServiceResponse()
                { Status = Status.Error, Message = "Internal Client Error" };
            }
        }

        public async Task<ServiceResponse> ForgotPasswordAsync(string email)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "forgot-password");
            request.Content = new StringContent(JsonSerializer.Serialize(new { email }), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage res = await _client.SendAsync(request);
                if (res.IsSuccessStatusCode)
                {
                    return new ServiceResponse()
                    { Status = Status.Success, Message = "Verification OTP sent to your email" };
                }
                else if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    return new ServiceResponse()
                    { Status = Status.Failed, Message = "Email not found" };
                }
                else
                {
                    return new ServiceResponse()
                    { Status = Status.Error, Message = "Internal Server Error" };
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new ServiceResponse()
                { Status = Status.Error, Message = "Internal Client Error" };
            }
        }

        public async Task<bool> VerifyAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "verify");
            request.Headers.Add("Cookie", RestService.GetCookies());

            HttpResponseMessage res = await _client.SendAsync(request);
            return res.IsSuccessStatusCode;
        }
    }
}
