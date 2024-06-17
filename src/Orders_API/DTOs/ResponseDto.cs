using System.Runtime.Serialization;

namespace Orders_API.DTOs;

[DataContract(Name = "Products in Order")]
public record ResponseDto<T>
{
    public T Data { get; set; } = default!;
    public bool Success { get; set; } = true;
    public string? Error { get; set; } = null;
}