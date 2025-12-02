using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly PurchaseService _purchaseService;

    public PurchaseController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseDto dto)
    {
        var purchase = await _purchaseService.CreatePurchase(dto);
        if (purchase == null)
            return BadRequest("Invalid purchase data. Check user exists and products have sufficient quantity.");

        return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.Id }, purchase);
    }

    [HttpGet]
    public async Task<ActionResult<List<PurchaseDto>>> GetAllPurchases()
    {
        var purchases = await _purchaseService.GetAllPurchases();
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseById(int id)
    {
        var purchase = await _purchaseService.GetPurchaseById(id);
        if (purchase == null)
            return NotFound($"Purchase with ID {id} not found.");

        return Ok(purchase);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<PurchaseDto>>> GetPurchasesByUserId(int userId)
    {
        var purchases = await _purchaseService.GetPurchasesByUserId(userId);
        return Ok(purchases);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePurchase(int id)
    {
        var result = await _purchaseService.DeletePurchase(id);
        if (!result)
            return NotFound($"Purchase with ID {id} not found.");

        return NoContent();
    }
}

