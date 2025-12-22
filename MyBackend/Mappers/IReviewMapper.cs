using MyBackend.DTOs.ReviewDtos;
using MyBackend.Models;

namespace MyBackend.Mappers;

public interface IReviewMapper
{
    ReviewDto? ToDto(ProductReview? review);
    ProductReview ToEntity(CreateReviewDto dto);
    void UpdateEntity(UpdateReviewDto dto, ProductReview review);
}