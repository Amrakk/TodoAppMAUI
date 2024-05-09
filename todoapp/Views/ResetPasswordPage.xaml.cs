using todoapp.ViewModels;

namespace todoapp.Views;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}
}