using todoapp.ViewModels;

namespace todoapp.Views;

public partial class TodosPage : ContentPage
{
	private readonly AuthViewModel _authViewModel;
    private readonly TodoViewModel _todoViewModel;

    public TodosPage(TodoViewModel todoViewModel, AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = todoViewModel;
		_authViewModel = authViewModel;
        _todoViewModel = todoViewModel;
        _todoViewModel.Username = _authViewModel.LoginModel.Username;
    }


    protected override async void OnAppearing()
    {
        await _todoViewModel.Init();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await _authViewModel.LogoutAsync();
    }
}