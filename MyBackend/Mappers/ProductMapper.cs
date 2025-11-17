using MyBackend.DTOs;
using MyBackend.Models;

namespace MyBackend.Mappers;

public class ProductMapper
{
    public ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }
}