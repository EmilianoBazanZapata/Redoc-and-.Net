namespace ApiRedoc.ViewModelsWithRedoc.Request;

public class TodoItemRequest
{
    public long Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Completado { get; set; } = false;
}