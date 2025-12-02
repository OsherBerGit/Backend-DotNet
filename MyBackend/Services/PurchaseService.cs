using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Mappers;
using MyBackend.Models;

namespace MyBackend.Services;

public class PurchaseService
{
    private readonly AppDbContext _context;
    private readonly PurchaseMapper _mapper;
    
    public PurchaseService(AppDbContext context, PurchaseMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PurchaseDto?> CreatePurchase(CreatePurchaseDto dto)
    {
        // Validate user exists
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists)
            return null;

        // Validate all products exist and have sufficient quantity
        foreach (var item in dto.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null || product.Quantity < item.Quantity)
                return null;
        }

        // Create purchase
        var purchase = new Purchase
        {
            UserId = dto.UserId,
            Date = DateTime.UtcNow
        };

        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        // Create purchase items and update product quantities
        foreach (var item in dto.Items)
        {
            var purchaseProduct = new PurchaseProduct
            {
                PurchaseId = purchase.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };

            _context.PurchaseProducts.Add(purchaseProduct);

            // Decrease product quantity
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product != null)
            {
                product.Quantity -= item.Quantity;
            }
        }

        await _context.SaveChangesAsync();

        // Reload purchase with all navigation properties for DTO mapping
        var createdPurchase = await _context.Purchases
            .Include(p => p.PurchaseProducts)
            .ThenInclude(pp => pp.Product)
            .FirstOrDefaultAsync(p => p.Id == purchase.Id);

        return createdPurchase != null ? _mapper.ToDto(createdPurchase) : null;
    }

    public async Task<List<PurchaseDto>> GetAllPurchases()
    {
        var purchases = await _context.Purchases
            .Include(p => p.PurchaseProducts)
            .ThenInclude(pp => pp.Product)
            .ToListAsync();

        return purchases.Select(_mapper.ToDto).ToList();
    }

    public async Task<PurchaseDto?> GetPurchaseById(int id)
    {
        var purchase = await _context.Purchases
            .Include(p => p.PurchaseProducts)
            .ThenInclude(pp => pp.Product)
            .FirstOrDefaultAsync(p => p.Id == id);

        return purchase != null ? _mapper.ToDto(purchase) : null;
    }

    public async Task<List<PurchaseDto>> GetPurchasesByUserId(int userId)
    {
        var purchases = await _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.PurchaseProducts)
            .ThenInclude(pp => pp.Product)
            .ToListAsync();

        return purchases.Select(_mapper.ToDto).ToList();
    }

    public async Task<bool> DeletePurchase(int id)
    {
        var purchase = await _context.Purchases
            .Include(p => p.PurchaseProducts)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (purchase == null)
            return false;

        // Restore product quantities
        foreach (var purchaseProduct in purchase.PurchaseProducts)
        {
            var product = await _context.Products.FindAsync(purchaseProduct.ProductId);
            if (product != null)
                product.Quantity += purchaseProduct.Quantity;
        }

        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync();

        return true;
    }
}