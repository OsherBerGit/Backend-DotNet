using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("Purchase")]
public class Purchase
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public User User { get; set; }
    
    // Many-to-many relationship with Product table
    public ICollection<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();
}