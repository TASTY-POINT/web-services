using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Ordering.Domain.Services;
using TastyPoint.API.Ordering.Resources;
using TastyPoint.API.Shared.Extensions;


namespace TastyPoint.API.Ordering.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class OrdersController:ControllerBase
{
     private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Orders",
        Description = "Get all the existing orders",
        OperationId = "GetOrder",
        Tags = new []{"Orders"}
    )]
    public async Task<IEnumerable<OrderResource>> GetAllAsync()
    {
        var orders = await _orderService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return resources;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Order by Id",
        Description = "Get existing order with specific Id",
        OperationId = "GetOrderById",
        Tags = new []{"Orders"}
    )]
    public async Task<OrderResource> GetByIdAsync(int id)
    {
        var order = await _orderService.FindByIdAsync(id);
        var resource = _mapper.Map<Order, OrderResource>(order.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Order",
        Description = "Add new order in the database",
        OperationId = "PostOrder",
        Tags = new []{"Orders"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var order = _mapper.Map<SaveOrderResource, Order>(resource);

        var result = await _orderService.SaveAsync(order);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order,OrderResource>(result.Resource);

        return Created(nameof(PostAsync), orderResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Order",
        Description = "Update some existing order by Id",
        OperationId = "PutOrder",
        Tags = new []{"Orders"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var order = _mapper.Map<SaveOrderResource, Order>(resource);

        var result = await _orderService.UpdateAsync(id, order);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);

        return Ok(orderResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Order",
        Description = "Delete some existing order by Id",
        OperationId = "DeleteOrder",
        Tags = new []{"Orders"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _orderService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);

        return Ok(orderResource);
    }
}
