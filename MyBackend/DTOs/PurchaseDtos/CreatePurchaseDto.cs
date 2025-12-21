using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs.PurchaseDtos;

public class CreatePurchaseDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Cart cannot be empty")]
    public List<CreatePurchaseItemDto> Items { get; set; } = new();
}