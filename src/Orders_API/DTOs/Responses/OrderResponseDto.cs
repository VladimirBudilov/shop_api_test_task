namespace Orders_API.DTOs.Responses;

public record OrderResponseDto
{
    public OrderResponseDto() { }

    public OrderResponseDto(Guid id, DateTime createdAt, List<ProductInOrderResponseDto> products)
    {
        Id = id;
        CreatedAt = createdAt;
        Products = products;
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<ProductInOrderResponseDto> Products { get; init; }
}