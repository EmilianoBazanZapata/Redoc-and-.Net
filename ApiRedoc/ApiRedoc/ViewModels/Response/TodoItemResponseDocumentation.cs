using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiRedoc.ViewModels.Response;

public class TodoItemResponseDocumentation
{
    [SwaggerSchema(Title = "Unique ID",
        Description = "This is the database ID and will be unique.",
        Format = "int")]
    public int Id { get; set; }
        
    [Description("Name of the task to complete")]
    public string Name { get; set; }

    [Description("Indicates whether the task was completed or not")]
    public bool Completed { get; set; }
}