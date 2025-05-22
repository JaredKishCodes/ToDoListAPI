using System.Globalization;
using ToDoListAPI.Models;

namespace ToDoListAPI.Data.Repository
{
    public interface ITodoRepository
    {
         Task<List<Todo>> GetTodos();
         Task<Todo> GetTodoById(int id);
         Task<List<Todo>> SearchTodo(string title, string description);
         Task<Todo> CreateTodo(Todo todo);
         Task<Todo> UpdateTodo(Todo todo);
         Task<bool> DeleteTodo(int id);
    }
}
