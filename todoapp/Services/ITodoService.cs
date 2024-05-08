using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoapp.Models;

namespace todoapp.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetTodosAsync();
        Task<bool> SaveTodoAsync(Todo item, bool isNewItem);
        Task<bool> DeleteTodoAsync(Todo item);
    }
}
