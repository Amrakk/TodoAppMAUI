using todoapp.Views;
using todoapp.Services;
using todoapp.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace todoapp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
            builder.Services.AddSingleton<RestService>();
            builder.Services.AddSingleton<ITodoService, TodoService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddSingleton<AuthViewModel>();
            builder.Services.AddSingleton<TodoViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<SignupPage>();
            //builder.Services.AddSingleton<ForgotPasswordPage>();
            //builder.Services.AddSingleton<ResetPasswordPage>();
            builder.Services.AddTransient<TodosPage>();
            builder.Services.AddTransient<LoadingPage>();

            return builder.Build();
        }
    }
}
