using AutoMapper;
using BLL.Services.Interfaces;
using DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using Orders_API.DTOs;
using Orders_API.DTOs.Requests;
using Orders_API.DTOs.Responses;

namespace Orders_API.Controllers;

/// <summary>
/// Endpoints for managing products.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService service, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <returns>A list of products.</returns>
    /// <response code="200">Returns the list of products.</response>
    [HttpGet]
    public async Task<ActionResult<ResponseDto<IEnumerable<ProductResponseDto>>>> GetProducts()
    {
        var products = await service.GetProductsAsync();
        var data = mapper.Map<IEnumerable<ProductResponseDto>>(products);
        var output = new ResponseDto<IEnumerable<ProductResponseDto>> { Data = data };
        return Ok(output);
    }
    
    /// <summary>
    /// Gets an product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <returns>The product with the given ID.</returns>
    /// <response code="200">Returns the product with the given ID.</response>
    /// <response code="400">If the product is not found.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<ProductResponseDto>>> GetProduct([FromRoute] Guid id)
    {
        var order = await service.GetProductAsync(id);
        var output = new ResponseDto<ProductResponseDto>();
        if (order == null)
        {
            output.Success = false;
            output.Error = "Product not found";
            return BadRequest(output);
        }

        var data = mapper.Map<ProductResponseDto>(order);
        output.Data = data;
        return Ok(output);
    }
    
    /// <summary>
    /// Creates a new product. Assign Title and Price to the product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <param name="productRequestDto"></param>
    /// <returns>The created product.</returns>
    /// <response code="200">Returns the created product.</response>
    /// <response code="400">If the product already exist.</response>
    [HttpPost]
    public async Task<ActionResult<ResponseDto<ProductResponseDto>>> CreateProduct([FromBody] ProductRequestDto productRequestDto)
    {
        var product = mapper.Map<Product>(productRequestDto);
        var newProduct = await service.AddProductAsync(product);
        var output = new ResponseDto<ProductResponseDto>();
        if (newProduct == null)
        {
            output.Success = false;
            output.Error = "Product already exists";
            return BadRequest(output);
        }

        var data = mapper.Map<ProductResponseDto>(newProduct);
        output.Data = data;
        return Ok(output);
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The id of the product to delete.</param>
    /// <returns>The deleted product.</returns>
    /// <response code="200">Returns the deleted product.</response>
    /// <response code="400">If the product is not found.</response>
    [HttpDelete( "{id}")]

    public async Task<ActionResult<ResponseDto<ProductResponseDto>>> DeleteProduct(Guid id)
    {
        var product = await service.DeleteProductAsync(id);
        var output = new ResponseDto<ProductResponseDto>();
        if (product == null)
        {
            output.Success = false;
            output.Error = "Product not found";
            return BadRequest(output);
        }

        var data = mapper.Map<ProductResponseDto>(product);
        output.Data = data;
        return Ok(output);
    }
}