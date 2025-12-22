using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs.ReviewDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [NonAction]
    public async Task<ActionResult<List<ReviewDto>>> GetAllReviews()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetReviewById(int id)
    {
        var review = await reviewService.GetReviewByIdAsync(id);
        if (review is null)
            return NotFound();
        return Ok(review);
    }
    
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto dto)
    {
        try
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            var newReview = await reviewService.CreateReviewAsync(userId, dto);
            if (newReview is null)
                return BadRequest("Failed to create review");
            
            return CreatedAtAction(nameof(GetReviewById), new { id = newReview.Id }, newReview);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<List<ReviewDto>>> GetReviewsByProductId(int productId)
    {
        var reviews = await reviewService.GetReviewsByProductIdAsync(productId);
        return Ok(reviews);
    }

    [HttpGet("my-reviews")]
    public async Task<ActionResult<List<ReviewDto>>> GetReviewsByUserId()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return Unauthorized("User ID not found in token");

        int userId = int.Parse(userIdClaim.Value);
        var reviews = await reviewService.GetReviewsByUserIdAsync(userId);
        return Ok(reviews);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ReviewDto?>> UpdateReview(int id, UpdateReviewDto dto)
    {
        try
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            var updatedProduct = await reviewService.UpdateReviewAsync(userId, id, dto);
            if (updatedProduct is null)
                return NotFound();
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        try
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            return await reviewService.DeleteReviewAsync(userId, id) ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}