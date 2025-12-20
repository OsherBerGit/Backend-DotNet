using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs;
using MyBackend.DTOs.ProductDtos;
using MyBackend.Exceptions;
using MyBackend.Mappers;

namespace MyBackend.Services;

public class ProductService(AppDbContext context, IProductMapper mapper) : IProductService
{
    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var existingProduct = await context.Products.AnyAsync(p => p.Name == dto.Name);
        if (existingProduct)
            throw new ProductAlreadyExistsException("Product with this name already exists.");
        
        var product = mapper.ToEntity(dto);

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return mapper.ToDto(product)!;
    }
    
    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await context.Products
            .AsNoTracking()
            .ToListAsync();
        
        return products.Select(p => mapper.ToDto(p)!).ToList();
    }
    
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return mapper.ToDto(product);
    }
    
    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
            return null;
        
        mapper.UpdateEntity(dto, product);

        await context.SaveChangesAsync();

        return mapper.ToDto(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
            return false;
        
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<ProductDto?> UpdateProductQuantityAsync(int id, int delta)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
            return null;
        
        if (product.Quantity + delta < 0)
            throw new InvalidOperationException("Quantity cannot be negative!");
        
        product.Quantity += delta; 

        await context.SaveChangesAsync();
        
        return mapper.ToDto(product);
    }
}