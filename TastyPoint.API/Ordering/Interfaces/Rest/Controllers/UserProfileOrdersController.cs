using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Ordering.Domain.Services;
using TastyPoint.API.Ordering.Resources;

namespace TastyPoint.API.Ordering.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/userprofile/{userProfileId}/orders")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProfileOrdersController: ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public UserProfileOrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Orders for given User Profile",
        Description = "Get existing Orders associated with specified User Profile",
        OperationId = "GetUserProfileOrders",
        Tags = new[] { "Orders" }
    )]
    public async Task<IEnumerable<OrderResource>> GetAllByUserProfileIdAsync(int userProfileId)
    {
        var orders = await _orderService.ListByUserProfileIdAsync(userProfileId);
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return resources;
    }
}