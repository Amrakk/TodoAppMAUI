namespace todoapp.Models
{
    public class GetTodoResponse
    {
        public string message { get; set; }
        public List<Todo> todos { get; set; }
    }
}
