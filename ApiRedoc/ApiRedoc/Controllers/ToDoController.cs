using ApiRedoc.ViewModelsWithRedoc.Request;
using Microsoft.AspNetCore.Mvc;

namespace ApiRedoc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private static readonly List<TodoItemRequest> todos = new List<TodoItemRequest>();
    
    [HttpGet]
    public ActionResult<IEnumerable<TodoItemRequest>> Get()
    {
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItemRequest> Get(int id)
    {
        var todo = todos.Find(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public ActionResult Post([FromBody] TodoItemRequest todo)
    {
        todo.Id = todos.Count + 1;
        todos.Add(todo);

        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] TodoItemRequest updatedTodo)
    {
        var todo = todos.Find(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }

        todo.Nombre = updatedTodo.Nombre;
        todo.Completado = updatedTodo.Completado;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var todo = todos.Find(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }

        todos.Remove(todo);

        return NoContent();
    }
}