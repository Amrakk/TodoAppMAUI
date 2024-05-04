using System.Net;
using System.Text;
using todoapp.Models;
using System.Text.Json;

namespace todoapp.Services
{
    public class TodoService : ITodoService
    {
        HttpClient _client;

        public TodoService(RestService service)
        {
            _client = service.GetClient;
            Console.WriteLine(_client);Console.WriteLine(_client);
            
        }

        public Task DeleteTaskAsync(Todo item)
        {

            throw new NotImplementedException();
        }

        public async Task<List<Todo>> GetTasksAsync()
        {
            //Console.WriteLine(_client.BaseAddress);
            //HttpContent content1 = new StringContent(JsonSerializer.Serialize(new { username = "adminduy", password = "Admin123" }), Encoding.UTF8, "application/json");
            //HttpResponseMessage loginres = await _client.PostAsync("login", content1);

            //Console.WriteLine(loginres.RequestMessage.ToString());
            //Console.WriteLine(loginres);
            //Console.WriteLine(await loginres.Content.ReadAsStringAsync() + "123");


            HttpResponseMessage res = await _client.GetAsync("todos");
            Console.WriteLine(123);
            Console.WriteLine(123);

            Console.WriteLine(res);
            Console.WriteLine(await res.Content.ReadAsStringAsync());
            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return JsonSerializer.Deserialize<GetTodoRespose>(json: content)!.todos;
            } else if(res.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Unauthorized");
            }
            return new List<Todo>();
        }

        public Task SaveTaskAsync(Todo item, bool isNewItem)
        {
            throw new NotImplementedException();
        }
    }
}
