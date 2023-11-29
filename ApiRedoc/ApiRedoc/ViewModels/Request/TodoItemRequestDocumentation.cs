using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiRedoc.ViewModels.Request
{
    [Description("Representa la solicitud para un elemento de tarea.")]
    public class TodoItemRequestDocumentation
    {
        [SwaggerSchema(Title = "Unique ID",
                       Description = "This is the database ID and will be unique.",
                       Format = "int")]
        public int Id { get; set; }
        
        [Description("Name of the task to complete")]
        [Required]
        public string Name { get; set; }

        [Description("Indicates whether the task was completed or not")]
        public bool Completed { get; set; }
    }
}