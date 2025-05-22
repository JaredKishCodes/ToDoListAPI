using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;

namespace ToDoListAPI.Data.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;
        public TodoRepository(AppDbContext context  )
        {
            _context = context;
        }

        public async Task<Todo> CreateTodo(Todo todo)
        {
            var result = await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Todo>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<List<Todo>> SearchTodo(string title, string description)
        {
            IQueryable<Todo> query = _context.Todos;

            if (!string.IsNullOrEmpty(title))  
            {
                query = query.Where(e => e.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(e => e.Description.Contains(description));
            }

            return await query.ToListAsync();
        }

        public async Task<Todo> UpdateTodo(Todo todo)
        {
            var results = await _context.Todos.FirstOrDefaultAsync(t => t.Id == todo.Id);
            
            if (results != null)
            {
                results.Id = todo.Id;
                results.Title = todo.Title; 
                results.Description = todo.Description;
                results.IsCompleted = todo.IsCompleted; 
                results.Created = todo.Created;

                await _context.SaveChangesAsync();
                return results;
            }

            return null;
        }
    }
}
