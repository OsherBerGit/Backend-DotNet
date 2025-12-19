using MyBackend.DTOs;
using MyBackend.DTOs.ProductDtos;

namespace MyBackend.Services;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteProductAsync(int id);
    Task<ProductDto?> UpdateProductQuantityAsync(int id, int delta);
}