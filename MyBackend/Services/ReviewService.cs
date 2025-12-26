using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs.ReviewDtos;
using MyBackend.Mappers;

namespace MyBackend.Services;

public class ReviewService(AppDbContext context, IReviewMapper mapper) : IReviewService
{
    public async Task<List<ReviewDto>> GetAllReviewsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ReviewDto?> GetReviewByIdAsync(int id)
    {
        var review = await context.ProductReviews
            .AsNoTracking()
            .Include(pr => pr.User)
            .Include(pr => pr.Product)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return mapper.ToDto(review);
    }

    public async Task<List<ReviewDto>> GetReviewsByProductIdAsync(int productId)
    {
        var reviews = await context.ProductReviews
            .AsNoTracking()
            .Where(pr => pr.ProductId == productId)
            .Include(pr => pr.User)
            .OrderByDescending(pr => pr.CreatedAt)
            .ToListAsync();
        
        return reviews.Select(r => mapper.ToDto(r)!).ToList();
    }

    public async Task<List<ReviewDto>> GetReviewsByUserIdAsync(int userId)
    {
        var reviews = await context.ProductReviews
            .AsNoTracking()
            .Where(pr => pr.UserId == userId)
            .Include(pr => pr.Product)
            .OrderByDescending(pr => pr.CreatedAt)
            .ToListAsync();
        
        return reviews.Select(r => mapper.ToDto(r)!).ToList();
    }
    
    public async Task<ReviewDto?> CreateReviewAsync(int userId, CreateReviewDto dto)
    {
        var user = await context.Users.FindAsync(userId);
        if (user is null)
            throw new Exception("User not found");
        
        var product = await context.Products.AnyAsync(p => p.Id == dto.ProductId);
        if (!product)
            throw new Exception("Product not found");
        
        var existingReview = await context.ProductReviews.AnyAsync(r => r.UserId == userId && r.ProductId == dto.ProductId);
        if (existingReview)
            throw new Exception("User has already reviewed this product");
        
        var review = mapper.ToEntity(dto);
        review.UserId = userId;
        review.CreatedAt = DateTime.UtcNow;
        review.User = user;
        
        context.ProductReviews.Add(review);
        await context.SaveChangesAsync();
        
        return mapper.ToDto(review);
    }

    public async Task<ReviewDto?> UpdateReviewAsync(int userId, int id, UpdateReviewDto dto)
    {
        var review = await context.ProductReviews
            .Include(pr => pr.User)
            .Include(pr => pr.Product)
            .FirstOrDefaultAsync(r => r.Id == id);
        
        if (review is null)
            return null;
        
        if (review.UserId != userId)
            throw new Exception("You are not authorized to update this review");
        
        mapper.UpdateEntity(dto, review);
        
        await context.SaveChangesAsync();
        
        return mapper.ToDto(review);
    }

    public async Task<bool> DeleteReviewAsync(int userId, int id)
    {
        var review = await context.ProductReviews.FindAsync(id);
        if (review is null)
            return false;
        
        if (review.UserId != userId)
            throw new Exception("You are not authorized to delete this review");
        
        context.ProductReviews.Remove(review);
        await context.SaveChangesAsync();
        
        return true;
    }
}