using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoapp.Models
{
    public class GetTodoRespose : Response
    {
        public List<Todo> todos { get; set; }
    }
}
