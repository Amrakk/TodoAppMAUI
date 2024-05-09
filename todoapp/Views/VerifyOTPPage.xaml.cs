using todoapp.ViewModels;

namespace todoapp.Views;

public partial class VerifyOTPPage : ContentPage
{
	public VerifyOTPPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}
}