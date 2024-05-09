using todoapp.Views;

namespace todoapp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(TodosPage), typeof(TodosPage));

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(VerifyOTPPage), typeof(VerifyOTPPage));
            Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));

        }
    }
}
