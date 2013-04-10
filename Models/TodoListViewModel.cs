using System.Collections.Generic;

namespace TodoList.Models
{
    public class TodoListViewModel
    {
        public List<Todo> Todos { get; set; }
        public Todo AddForm { get; set; }
    }
}