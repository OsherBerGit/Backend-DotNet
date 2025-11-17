using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs;
using MyBackend.DTOs.ProductDtos;
using MyBackend.Mappers;
using MyBackend.Models;

namespace MyBackend.Services;

public class ProductService
{
    private readonly AppDbContext _context;
    private readonly ProductMapper _mapper;

    public ProductService(AppDbContext context, ProductMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ProductDto CreateProduct(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Quantity = dto.Quantity
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return _mapper.ToDto(product);
    }
    
    public List<ProductDto> GetAllProducts()
    {
        return _context.Products
            .Select(_mapper.ToDto)
            .ToList();
    }
    
    public ProductDto? GetProductById(int id)
    {
        var product = _context.Products.Find(id);
        return product is null ? null : _mapper.ToDto(product);
    }
    
    public ProductDto? UpdateProduct(int id, UpdateProductDto dto)
    {
        var product = _context.Products.Find(id);
        if (product == null)
            return null;

        if (dto.Name != null) product.Name = dto.Name;

        product.Description = dto.Description;

        if (dto.Price.HasValue) product.Price = dto.Price.Value;

        if (dto.Quantity.HasValue) product.Quantity = dto.Quantity.Value;

        _context.SaveChanges();

        return _mapper.ToDto(product);
    }

    public bool DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;
        
        _context.Products.Remove(product);
        _context.SaveChanges();
        
        return true;
    }
}