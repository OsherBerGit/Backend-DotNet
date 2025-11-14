using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;

namespace MyBackend.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context) { _context = context; }
    
    public Product CreateProduct(string name, string description, decimal price, int quantity)
    {
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Quantity = quantity
        };

        _context.Products.Add(product);
        _context.SaveChanges(); // synchronous save
        return product;
    }
    
    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }
    
    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }
    
    public void DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}