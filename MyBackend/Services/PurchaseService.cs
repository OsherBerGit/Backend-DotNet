using MyBackend.Data;

namespace MyBackend.Services;

public class PurchaseService
{
    private readonly AppDbContext _context;
    
    public PurchaseService(AppDbContext context) { _context = context; }
}