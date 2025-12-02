namespace MyBackend.DTOs.PurchaseDtos;

public class CreatePurchaseDto
{
    public int UserId { get; set; }
    public List<CreatePurchaseItemDto> Items { get; set; } = new();
}