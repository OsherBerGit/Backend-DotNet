using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs.PurchaseDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchaseController(IPurchaseService purchaseService) : ControllerBase
{
    [NonAction] // change to HttpGet
    public async Task<ActionResult<List<PurchaseDto>>> GetAllPurchases() { throw new NotImplementedException(); }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseById(int id)
    {
        var purchase = await purchaseService.GetPurchaseByIdAsync(id);
        if (purchase is null)
            return NotFound();
        return Ok(purchase);
    }
    
    [HttpPost]
    public async Task<ActionResult<PurchaseDto>> CreatePurchase([FromBody] CreatePurchaseDto dto)
    {
        try
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            var newPurchase = await purchaseService.CreatePurchaseAsync(userId, dto);
            if (newPurchase is null)
                return BadRequest("Failed to create purchase");
            
            return CreatedAtAction(nameof(GetPurchaseById), new { id = newPurchase.Id }, newPurchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<PurchaseDto>>> GetPurchasesByUserId(int userId)
    {
        var purchases = await purchaseService.GetPurchasesByUserIdAsync(userId);
        return Ok(purchases);
    }
    
    [HttpGet("my-purchases")]
    public async Task<ActionResult<List<PurchaseDto>>> GetMyPurchases()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return Unauthorized("User ID not found in token");

        int userId = int.Parse(userIdClaim.Value);
        var purchases = await purchaseService.GetPurchasesByUserIdAsync(userId);
        return Ok(purchases);
    }

    [NonAction]
    // [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePurchase(int id) { throw new NotImplementedException(); }
}

