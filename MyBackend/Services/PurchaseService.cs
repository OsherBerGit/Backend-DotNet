using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Mappers;
using MyBackend.Models;

namespace MyBackend.Services;

public class PurchaseService(AppDbContext context, IPurchaseMapper mapper) : IPurchaseService
{
    public async Task<PurchaseDto?> CreatePurchaseAsync(int userId, CreatePurchaseDto dto)
    {
        var user = await context.Users.FindAsync(userId);
        if (user is null) throw new Exception("User not found");
        
        var purchase = mapper.ToEntity(dto);
        purchase.UserId = userId;
        purchase.Date = DateTime.UtcNow;

        foreach (var item in purchase.PurchaseProducts)
        {
            var existingProduct = await context.Products.FindAsync(item.ProductId);
            if (existingProduct is null)
                throw new Exception($"Product with ID {item.ProductId} not found.");
            
            if (existingProduct.Quantity < item.Quantity)
                throw new Exception($"Product with ID {item.ProductId} has only {existingProduct.Quantity} left.");
            
            existingProduct.Quantity -= item.Quantity;
            item.Product = existingProduct;
        }
        
        context.Purchases.Add(purchase);
        await context.SaveChangesAsync();
        
        return mapper.ToDto(purchase);
    }

    public async Task<List<PurchaseDto>> GetAllPurchasesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PurchaseDto?> GetPurchaseByIdAsync(int id)
    {
        var purchase = await context.Purchases
            .AsNoTracking()
            .Include(p => p.PurchaseProducts)
            .ThenInclude(pp => pp.Product)
            .FirstOrDefaultAsync(p => p.Id == id);

        return mapper.ToDto(purchase);
    }

    public async Task<List<PurchaseDto>> GetPurchasesByUserIdAsync(int userId)
    {
        var purchases = await context.Purchases
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Include(p => p.PurchaseProducts)
            .ThenInclude(pp => pp.Product)
            .OrderByDescending(p => p.Date)
            .ToListAsync();
        
        return purchases.Select(p => mapper.ToDto(p)!).ToList();
    }

    public async Task<bool> DeletePurchaseAsync(int id)
    {
        throw new NotImplementedException();
    }
}