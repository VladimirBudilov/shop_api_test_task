using AutoMapper;
using BLL.Services.Interfaces;
using DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using Orders_API.DTOs;
using Orders_API.DTOs.Requests;
using Orders_API.DTOs.Responses;

namespace Orders_API.Controllers;

/// <summary>
/// Endpoints for managing orders.
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrdersController(IOrderService service, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Gets all orders.
    /// </summary>
    /// <returns>A list of orders.</returns>
    /// <response code="200">Returns the list of orders.</response>
    [HttpGet]
    public async Task<ActionResult<ResponseDto<IEnumerable<OrderResponseDto>>>> GetOrders()
    {
        var orders = await service.GetOrdersAsync();
        var data = mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        var output = new ResponseDto<IEnumerable<OrderResponseDto>>
        {
            Data = data,
            Success = true,
            Error = null
        };
        return Ok(output);
    }

    /// <summary>
    /// Gets an order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <returns>The order with the given ID.</returns>
    /// <response code="200">Returns the order with the given ID.</response>
    /// <response code="400">If the order is not found.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<OrderResponseDto>>> GetOrder([FromRoute]Guid id)
    {
        var output = new ResponseDto<OrderResponseDto>();
        
        var order = await service.GetOrderAsync(id);
        if (order == null)
        {
            output.Success = false;
            output.Error = "Order not found";
            return BadRequest(output);
        }

        var data = mapper.Map<OrderResponseDto>(order);
        output.Data = data;
        return Ok(output);
    }

    /// <summary>
    /// Creates a new order. Assign Array of valid ProductId's.
    /// </summary>
    /// <param name="order">The order to create.</param>
    /// <param name="orderRequestDto"></param>
    /// <returns>The created order.</returns>
    /// <response code="200">Returns the created order.</response>
    /// <response code="400">If the order already exist.</response>
    [HttpPost]
    public async Task<ActionResult<ResponseDto<OrderResponseDto>>> CreateOrder([FromBody] OrderRequestDto orderRequestDto)
    {
        var output = new ResponseDto<OrderResponseDto>();
        
        var order = mapper.Map<Order>(orderRequestDto);
        var newOrder = await service.CreateOrderAsync(order);
        if (newOrder == null)
        {
            output.Success = false;
            output.Error = "Invalid data";

            return BadRequest(output);
        }
        var data = mapper.Map<OrderResponseDto>(newOrder);
        output.Data = data;
        return Ok(output);
    }

    /// <summary>
    /// Deletes an order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns>The deleted order.</returns>
    /// <response code="200">Returns the deleted order.</response>
    /// <response code="400">If the order is not found.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<OrderResponseDto>>> DeleteOrder(Guid id)
    {
        var order = await service.DeleteOrderAsync(id);
        var output = new ResponseDto<OrderResponseDto>();
        if (order == null)
        {
            output.Success = false;
            output.Error = "Order not found";
            return BadRequest(output);
        }

        var data = mapper.Map<OrderResponseDto>(order);
        output.Data = data;
        return Ok(output);
    }
}