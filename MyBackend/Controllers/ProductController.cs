using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.DTOs.ProductDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
    {
        return await productService.GetAllProductsAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto?>> GetProductById(int id)
    {
        return await productService.GetProductByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto?>> CreateProduct(CreateProductDto dto)
    {
        return await productService.CreateProductAsync(dto);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto?>> UpdateProduct(int id, UpdateProductDto dto)
    {
        return await productService.UpdateProductAsync(id, dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProduct(int id)
    {
        return await productService.DeleteProductAsync(id);
    }
    
    [HttpPatch("{id}/quantity/{delta}")]
    public async Task<ActionResult<ProductDto?>> UpdateProductQuantity(int id, int delta)
    {
        return await productService.UpdateProductQuantityAsync(id, delta);
    }
}