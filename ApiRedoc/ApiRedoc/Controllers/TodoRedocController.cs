using ApiRedoc.ViewModels.Request;
using ApiRedoc.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiRedoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoRedocController : ControllerBase
    {
        [SwaggerOperation(
            Summary = "Get All Tasks",
            Description = "This endpoint will return all tasks, regardless of whether they are complete or not.",
            OperationId = "Get",
            Tags = new[] { "ToDoRedoc" })]
        [SwaggerResponse(200, "All Tasks registered", type: typeof(List<TodoItemResponseDocumentation>))]
        [SwaggerResponse(404, "Tasks Not Found")]
        [SwaggerResponse(500, "Internal Error")]
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemRequestDocumentation>> Get()
        {
            var example = new List<TodoItemRequestDocumentation>()
            {
                new TodoItemRequestDocumentation()
                {
                    Id = 1,
                    Name = "Task Test Data",
                    Completed = false
                },
                new TodoItemRequestDocumentation()
                {
                    Id = 2,
                    Name = "Task Test Data",
                    Completed = true
                },
                new TodoItemRequestDocumentation()
                {
                    Id = 3,
                    Name = "Task Test Data",
                    Completed = true
                }
            };
            return Ok(example);
        }

        [SwaggerOperation(
            Summary = "Get Specific Task by Id",
            Description = "This endpoint will return Task by Id, regardless of whether they are complete or not.",
            OperationId = "GetById",
            Tags = new[] { "ToDoRedoc" })]
        [HttpGet("{id}")]
        public ActionResult<TodoItemRequestDocumentation> Get(int id)
        {
            var example = new TodoItemRequestDocumentation()
            {
                Id = 1,
                Name = "Task Test Data",
                Completed = false
            };

            return Ok(example);
        }

        [SwaggerOperation(
            Summary = "Add Specific Task by Id",
            Description = "This endpoint will return all tasks, regardless of whether they are complete or not, " +
                          "and will validate that the new task name is not registered.",
            OperationId = "Post",
            Tags = new[] { "ToDoRedoc" })]
        [HttpPost]
        public ActionResult Post([FromBody] TodoItemRequestDocumentation todo)
        {
            return NoContent();
        }

        [SwaggerOperation(
            Summary = "Update Specific Task by Id",
            Description =
                "This endpoint will update a specific tasks by id, regardless of whether they are complete or not, " +
                "and will validate that the new task name is not registered.",
            OperationId = "Put",
            Tags = new[] { "ToDoRedoc" })]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItemRequestDocumentation updatedTodo)
        {
            return NoContent();
        }

        [SwaggerOperation(
            Summary = "Delete Specific Task by Id",
            Description = "This endpoint will Delete Task by Id, regardless of whether they are complete or not.",
            OperationId = "Delete",
            Tags = new[] { "ToDoRedoc" })]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}