using System.ComponentModel.DataAnnotations;

namespace Orders_API.DTOs.Requests;

public record ProductRequestDto(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Title,

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    decimal Price
);