using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs.PurchaseDtos;

public class CreatePurchaseItemDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}