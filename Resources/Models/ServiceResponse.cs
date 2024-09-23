namespace Resources.Models;

public class ServiceResponse
{
    public bool Succeeded { get; set; }
    public object? Content { get; set; }
    public string? Message { get; set; }
}
