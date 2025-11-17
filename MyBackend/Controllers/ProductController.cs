using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.DTOs.ProductDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController
{
    private readonly ProductService _productService;
    
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("/products")]
    public ActionResult<List<ProductDto>> GetAllProducts()
    {
        return _productService.GetAllProducts();
    }
    
    [HttpGet("/products/{id}")]
    public ActionResult<ProductDto?> GetProductById(int id)
    {
        return _productService.GetProductById(id);
    }

    [HttpPost("/products")]
    public ActionResult<ProductDto?> CreateProduct(CreateProductDto dto)
    {
        return _productService.CreateProduct(dto);
    }
    
    [HttpPut("/products/{id}")]
    public ActionResult<ProductDto?> UpdateProduct(int id, UpdateProductDto dto)
    {
        return _productService.UpdateProduct(id, dto);
    }
    
    [HttpDelete("/products/{id}")]
    public ActionResult<bool> DeleteProduct(int id)
    {
        return _productService.DeleteProduct(id);
    }
}