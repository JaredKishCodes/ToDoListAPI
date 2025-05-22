using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Data.Repository;
using ToDoListAPI.Models;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodolistController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        public TodolistController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllTodosAsync()
        {
            try
            {   
                return Ok(await _todoRepository.GetTodos());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}", Name = "GetTodoById")]
        public async Task<ActionResult<Todo>> GetByIdAsync(int id)
        {

            try
            {
                if (id <= 0)
                {
                    return BadRequest("Enter valid ID");
                }

                var todo = await _todoRepository.GetTodoById(id);

                if (todo == null)
                {
                    return NotFound("Todo not found");
                }

                return Ok(todo);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Todo>>> SearchTodoAsync([FromQuery] string? title, string? description)
        {
            try
            {
                var result = await _todoRepository.SearchTodo(title, description);

                if (result?.Any() == true)
                {
                    return Ok(result);
                }

                return NotFound("No matching todos found.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }



        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodoAsync(Todo todo)
        {
            try
            {
                if (todo == null)
                {
                    return BadRequest("Failed to create Todo");
                }
                todo.Created = DateTime.Now;

                var result = await _todoRepository.CreateTodo(todo);

                if (result != null)
                {
                    return CreatedAtRoute("GetTodoById", new { id = result.Id }, result);

                }

                return BadRequest("Failed to create due to unknown error");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateToDoAsync(int id, Todo todo)
        {
            try
            {
                if (id != todo.Id)
                {
                    return BadRequest("Todo Id mismatch");
                }

                var existingToDo = await _todoRepository.GetTodoById(id);

                if (existingToDo == null)
                {
                    return NotFound($"Todo ID with the ID: {id} can not found");
                }

               var updatedToDo =  await _todoRepository.UpdateTodo(todo);
                return Ok(updatedToDo);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error updating data from the database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTodoAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Enter valid ID");
                }

                var toBeDeleted = await _todoRepository.GetTodoById(id);

                if (toBeDeleted == null)
                {
                    return NotFound($"Todo ID with the id : {id} not found");
                }

                await _todoRepository.DeleteTodo(id);

                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error deleting data from the database");
            }
            

        }
    }
}
