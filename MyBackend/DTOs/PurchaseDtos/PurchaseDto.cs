namespace MyBackend.DTOs.PurchaseDtos;

public class PurchaseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}