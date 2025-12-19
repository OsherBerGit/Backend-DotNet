using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Mappers;
using MyBackend.Models;

namespace MyBackend.Services;

public class PurchaseService(AppDbContext context, IPurchaseMapper mapper) : IPurchaseService
{
    public async Task<PurchaseDto?> CreatePurchaseAsync(CreatePurchaseDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PurchaseDto>> GetAllPurchasesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PurchaseDto?> GetPurchaseByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PurchaseDto>> GetPurchasesByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletePurchaseAsync(int id)
    {
        throw new NotImplementedException();
    }
}