using MyBackend.DTOs;
using MyBackend.DTOs.ProductDtos;
using MyBackend.Models;

namespace MyBackend.Mappers;

public interface IProductMapper
{
    ProductDto? ToDto(Product? product);
    Product ToEntity(CreateProductDto dto);
    void UpdateEntity(UpdateProductDto dto, Product entity);
}