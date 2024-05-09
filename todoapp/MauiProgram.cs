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
#if ANDROID
                .ConfigureMauiHandlers(handlers => handlers.AddHandler<Entry, PINView.Maui.Platforms.Android.Handlers.EntryHandler>())
#endif
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

            builder.Services.AddTransient<TodosPage>();
            builder.Services.AddTransient<LoadingPage>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<SignupPage>();
            builder.Services.AddTransient<VerifyOTPPage>();
            builder.Services.AddTransient<ResetPasswordPage>();
            builder.Services.AddSingleton<ForgotPasswordPage>();

            return builder.Build();
        }
    }
}
