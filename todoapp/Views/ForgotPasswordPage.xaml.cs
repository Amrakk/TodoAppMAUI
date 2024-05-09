using todoapp.ViewModels;

namespace todoapp.Views;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}
}