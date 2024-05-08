using System.Windows.Input;
using todoapp.ViewModels;

namespace todoapp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		this.BindingContext = authViewModel;
		
	}
}