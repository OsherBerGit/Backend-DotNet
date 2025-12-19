using MyBackend.DTOs.PurchaseDtos;

namespace MyBackend.Services;

public interface IPurchaseService
{
    Task<PurchaseDto> CreatePurchaseAsync(CreatePurchaseDto dto);
    Task<List<PurchaseDto>> GetAllPurchasesAsync();
    Task<PurchaseDto?> GetPurchaseByIdAsync(int id);
    Task<bool> DeletePurchaseAsync(int id);
    Task<List<PurchaseDto>> GetPurchasesByUserId(int userId);
}