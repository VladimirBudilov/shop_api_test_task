namespace Orders_API.DTOs.Responses;

public record ProductInOrderResponseDto(ProductResponseDto Product, long Quantity);
