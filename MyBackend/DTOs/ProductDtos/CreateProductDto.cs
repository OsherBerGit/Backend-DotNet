using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs.ProductDtos;

public class CreateProductDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}