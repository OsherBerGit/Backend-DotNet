using Microsoft.AspNetCore.Authorization;
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
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto?>> CreateProduct(CreateProductDto dto)
    {
        try
        {
            var newProduct = await productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto?>> UpdateProduct(int id, UpdateProductDto dto)
    {
        try
        {
            var updatedProduct = await productService.UpdateProductAsync(id, dto);
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var success = await productService.DeleteProductAsync(id);
            return success ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPatch("{id}/quantity/{delta}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> UpdateProductQuantity(int id, int delta)
    {
        try
        {
            var updatedProduct = await productService.UpdateProductQuantityAsync(id, delta);
            if (updatedProduct is null)
                return NotFound();
            return Ok(updatedProduct); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}