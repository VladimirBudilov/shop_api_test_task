using System.ComponentModel.DataAnnotations;

namespace Orders_API.DTOs.Requests;

public record OrderRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one product is required")]
    public List<ProductInOrderRequestDto> Products { get; set; }
}