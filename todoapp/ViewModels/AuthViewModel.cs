using todoapp.Views;
using todoapp.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using todoapp.Models;
using System.Text.RegularExpressions;

namespace todoapp.ViewModels
{
    public partial class AuthViewModel : BaseViewModel
    {
        [ObservableProperty]
        private LoginModel _loginModel;

        [ObservableProperty]
        private SignupModel _signupModel;

        [ObservableProperty]
        private ResetPasswordModel _forgotPasswordModel;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string? _errorMessages;

        [ObservableProperty]
        private bool _isAuthenticated;

        readonly IAuthService _authService;

        public AuthViewModel(IAuthService authService)
        {
            _authService = authService;
            _loginModel = new ();
            _signupModel = new ();
            _forgotPasswordModel = new ();
            _isAuthenticated = false;
            _email = "";
            _loginModel.Username = Preferences.Get("username", "");
        }

        [RelayCommand]
        public async Task Login()
        {
            var Username = LoginModel.Username; 
            var Password = LoginModel.Password;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessages = "Username and Password are required";
                return;
            }

            ServiceResponse result = await _authService.LoginAsync(Username, Password);
            if (result.Status == Status.Success)
            {
                ErrorMessages = "";
                IsAuthenticated = true;
                Preferences.Set("username", Username);
                
                await Shell.Current.GoToAsync(nameof(TodosPage));
                return;
            }
            else if (result.Status == Status.Error) ErrorMessages = "Oops! Something went wrong. Please try again later";
            else ErrorMessages = result.Message;
        }

        [RelayCommand]
        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            Preferences.Set("ref_token", "");
            Preferences.Set("access_token", "");
            IsAuthenticated = false;
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        [RelayCommand]
        public async Task Signup()
        {
            var Email = SignupModel.Email;
            var Username = SignupModel.Username;
            var Password = SignupModel.Password;
            var ConfirmPassword = SignupModel.ConfirmPassword;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ErrorMessages = "All fields are required";
                return;
            }

            if(!Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$"))
            {
                ErrorMessages = "Invalid email address";
                return;
            }

            if(!Regex.IsMatch(Username, @"^[a-z0-9]+$"))
            {
                ErrorMessages = "Username can only contain lowercase letters and numbers";
                return;
            }

            if(!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[a-zA-Z0-9]{8,}$"))
            {
                ErrorMessages = "Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number and be at least 8 characters long";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessages = "Passwords do not match";
                return;
            }

            ServiceResponse result = await _authService.SignupAsync(Email, Username, Password);
            if (result.Status == Status.Success)
            {
                ErrorMessages = "";
                await App.Current.MainPage.DisplayAlert("Success", "Account created successfully!\nPlease check your email to activate your account", "Ok");
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }
            else if (result.Status == Status.Error) ErrorMessages = "Oops! Something went wrong. Please try again later";
            else ErrorMessages = result.Message;
        }

        [RelayCommand]
        public async Task NavigateToSignup()
        {
            Console.WriteLine("Navigating to Signup");
            ErrorMessages = "";
            await Shell.Current.GoToAsync(nameof(SignupPage));
        }

        [RelayCommand]
        public async Task NavigateToLogin()
        {
            ErrorMessages = "";
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(250);
            return await _authService.VerifyAsync();
        }
    }
}
