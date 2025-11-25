namespace MyBackend.DTOs.PurchaseDtos;

public class CreatePurchaseDto
{
    public int UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}