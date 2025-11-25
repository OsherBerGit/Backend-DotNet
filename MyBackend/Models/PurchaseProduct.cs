using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

// Many-to-many relationship between Purchases and Products
[Table("PurchaseProducts")]
public class PurchaseProduct
{ 
    // Composite keys
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive")]
    public int Quantity { get; set; }
    
    // Navigation properties
    public Purchase Purchase { get; set; }
    public Product Product { get; set; }
}