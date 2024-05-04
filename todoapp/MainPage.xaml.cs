using todoapp.Services;

namespace todoapp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        ITodoService _todoService;

        public MainPage(ITodoService todoService)
        {
            InitializeComponent();
            _todoService = todoService;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            var tasks = _todoService.GetTasksAsync();
            await tasks;
        }
    }

}
