using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

// Many-to-many relationship between Purchases and Products
[Table("PurchaseProducts")]
public class PurchaseProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(PurchaseId))]
    public int PurchaseId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public int ProductId { get; set; }
    
    [Column, Required]
    public int Quantity { get; set; }
    
    public Purchase Purchase { get; set; }
    public Product Product { get; set; }
}