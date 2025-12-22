using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("ProductReviews")]
public class ProductReview
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1,5)]
    public int Rating { get; set; }

    [MaxLength(500)]
    public string? Comment { get; set; }

    // Foreign key for Product
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    // Foreign key for User
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}