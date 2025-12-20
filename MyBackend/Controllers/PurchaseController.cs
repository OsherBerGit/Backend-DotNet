using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchaseController(IPurchaseService purchaseService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PurchaseDto>> CreatePurchase([FromBody] CreatePurchaseDto dto)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<List<PurchaseDto>>> GetAllPurchases()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("/user/{userId}")]
    public async Task<ActionResult<List<PurchaseDto>>> GetPurchasesByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePurchase(int id)
    {
        throw new NotImplementedException();
    }
}

