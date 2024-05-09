#if ANDROID 
using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif 

namespace todoapp
{
    public partial class App : Application
    {
        public App()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) => {
#if ANDROID 
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                handler.PlatformView.TextCursorDrawable.SetTint(Colors.DeepSkyBlue.ToAndroid());
#endif 
            });

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
