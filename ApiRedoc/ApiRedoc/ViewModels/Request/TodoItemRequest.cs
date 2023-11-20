namespace ApiRedoc.ViewModels.Request;

public class TodoItemRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Completed { get; set; }
}