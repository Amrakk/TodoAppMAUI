using System.Net;
using todoapp.Models;
using System.Text.Json;
using System.Text;

namespace todoapp.Services
{
    public class TodoService : ITodoService
    {
        HttpClient _client;

        public TodoService(RestService service)
        {
            _client = service.GetClient;
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            HttpRequestMessage request = new(HttpMethod.Get, "todos");
            request.Headers.Add("Cookie", RestService.GetCookies());
            HttpResponseMessage res = await _client.SendAsync(request); 
            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                RestService.SaveCookies(res);
                return JsonSerializer.Deserialize<GetTodoResponse>(json: content)!.todos;
            } else if(res.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            return new List<Todo>();
        }

        public async Task<bool> SaveTodoAsync(Todo todo, bool isNewItem)
        {
            HttpRequestMessage request;
            if(isNewItem) 
                request = new(HttpMethod.Post, "todos");
            else
                request = new(HttpMethod.Put, "todos");
            request.Headers.Add("Cookie", RestService.GetCookies());
            request.Content = isNewItem ? new StringContent(
                JsonSerializer.Serialize(new { content = todo.content }),
                Encoding.UTF8,
                "application/json"
            ) : new StringContent(
                JsonSerializer.Serialize(new { todo }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage res = await _client.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                RestService.SaveCookies(res);
                return true;
            }
            else if(res.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            return false;
        }

        public async Task<bool> DeleteTodoAsync(Todo todo)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, "todos");
            request.Headers.Add("Cookie", RestService.GetCookies());
            Console.WriteLine(todo.id);
            request.Content = new StringContent(
                JsonSerializer.Serialize(new { todoIDs = new string[] { todo.id } }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage res = await _client.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                RestService.SaveCookies(res);
                return true;
            }
            else if (res.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            return false;
        }
    }
}

