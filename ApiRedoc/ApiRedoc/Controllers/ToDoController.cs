using ApiRedoc.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;

namespace ApiRedoc.Controllers;

[ApiExplorerSettings(GroupName = "swagger")]
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
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
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public ActionResult Post([FromBody] TodoItemRequest todo)
    {
        var todoExist = todos.Find(t => t.Name == todo.Name);

        if (todoExist != null)
            return Conflict($"Already exists the Todo: { todo.Name }");
        
        todo.Id = todos.Count + 1;
        todos.Add(todo);

        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] TodoItemRequest updatedTodo)
    {
        var todo = todos.Find(t => t.Id == id);
        
        if (todo == null)
            return NotFound();

        todo.Name = updatedTodo.Name;
        todo.Completed = updatedTodo.Completed;
        
        var todoExist = todos.Find(t => t.Name == todo.Name);

        if (todoExist != null)
            return Conflict($"Already exists the Todo: { todo.Name }");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var todo = todos.Find(t => t.Id == id);
        
        if (todo == null)
            return NotFound();
        todos.Remove(todo);

        return NoContent();
    }
}