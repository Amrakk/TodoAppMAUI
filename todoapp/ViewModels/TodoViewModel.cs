using todoapp.Models;
using todoapp.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace todoapp.ViewModels
{
    public partial class TodoViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        ObservableCollection<Todo> _todoList;

        [ObservableProperty]
        private string _todoEntryText;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private string _emptyText;

        private readonly ITodoService _todoService;

        public TodoViewModel(ITodoService todoService)
        {
            _todoService = todoService;
            TodoList = new ObservableCollection<Todo>();
            EmptyText = "No todos here. Add new Todo to get started 💪";
            TodoEntryText = "";
            Username = string.Empty;
        }

        public async Task Init()
        {
            GetTodos();
            if (TodoList.Count == 0)
            {
                EmptyText = "Loading projects..";
                await Task.Delay(2000);
                GetTodos();
                EmptyText = "No todos here. Add new Todo to get started 💪";
            }
        }

        [RelayCommand]
        public async void GetTodos()
        {
            IsRefreshing = true;
            IsBusy = true;

            try
            {
                var todos = (await _todoService.GetTodosAsync()).Reverse<Todo>().OrderBy(x => x.completed);
                TodoList.Clear();
                foreach(var todo in todos)
                {
                    TodoList.Add(todo);
                }
            } catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }

            IsRefreshing = false;
            IsBusy = false;
        }

        [RelayCommand]
        public async Task CheckTodo(Todo todo)
        {
            IsBusy = true;

            try
            {
                todo.completed = !todo.completed;
                if (await _todoService.SaveTodoAsync(todo, false))
                    GetTodos();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }

            IsBusy = false;
        }

        [RelayCommand]
        public async Task AddTodo()
        {
            IsBusy = true;

            try
            {
                Todo todo = new Todo() { content = TodoEntryText };
                TodoEntryText = "";
                if (await _todoService.SaveTodoAsync(todo, true))
                    GetTodos();
            } catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }

            IsBusy = false;
        }

        [RelayCommand]
        public async Task EditTodo(Todo todo)
        {
            string newString = await App.Current.MainPage.DisplayPromptAsync("Editing", todo.content);

            try
            {
                if (newString == null)
                    await App.Current.MainPage.DisplayAlert("Error", "Content cannot be empty", "Ok");
                else if (newString != todo.content)
                {
                    todo.content = newString;
                    if (await _todoService.SaveTodoAsync(todo, false))
                        GetTodos();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }

            IsBusy = false;
        }

        [RelayCommand]
        public async Task DeleteTodo(Todo todo)
        {
            IsBusy = true;

            try
            {
                if (await _todoService.DeleteTodoAsync(todo))
                    TodoList.Remove(todo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }

            IsBusy = false;
        }
    }
}


