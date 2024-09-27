namespace Resources.Models;

public class ServiceResponse<T> where T : class
{
    public bool Succeeded { get; set; }

    public object? Content { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
}
