using todoapp.ViewModels;

namespace todoapp.Views;

public partial class LoadingPage : ContentPage
{
    private readonly AuthViewModel _authViewModel;

    public LoadingPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		_authViewModel = authViewModel;
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if(await _authViewModel.IsAuthenticatedAsync())
            await Shell.Current.GoToAsync(nameof(TodosPage));
        else
            await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}