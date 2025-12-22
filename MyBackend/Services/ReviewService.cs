using MyBackend.Data;
using MyBackend.DTOs.ReviewDtos;
using MyBackend.Mappers;

namespace MyBackend.Services;

public class ReviewService(AppDbContext context, IReviewMapper mapper) : IReviewService
{
    public Task<ReviewDto?> CreateReviewAsync(int userId, CreateReviewDto? dto)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewDto>> GetAllReviewsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ReviewDto?> GetReviewByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewDto>> GetReviewsByProductIdAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewDto>> GetReviewsByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewDto?> UpdateReviewAsync(int id, UpdateReviewDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteReviewAsync(int id)
    {
        throw new NotImplementedException();
    }
}