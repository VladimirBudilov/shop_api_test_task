using System.ComponentModel.DataAnnotations;

namespace Orders_API.DTOs.Requests;

public record ProductInOrderRequestDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public long Quantity { get; set; }
}