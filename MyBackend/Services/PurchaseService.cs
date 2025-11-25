using MyBackend.Data;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Models;

namespace MyBackend.Services;

public class PurchaseService
{
    private readonly AppDbContext _context;
    
    public PurchaseService(AppDbContext context) { _context = context; }
    
    public async Task<PurchaseDto> CreatePurchase(CreatePurchaseDto dto)
    { 
        return null;
    }
}