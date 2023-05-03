using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Services;
using TastyPoint.API.Social.Resources;

namespace TastyPoint.API.Social.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/food-store/{foodStoreId}/comments")]
[Produces(MediaTypeNames.Application.Json)]
public class FoodStoreCommentsController: ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public FoodStoreCommentsController(IMapper mapper, ICommentService commentService)
    {
        _mapper = mapper;
        _commentService = commentService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Comments for given Food Store",
        Description = "Get existing Comments associated with specified FoodStore",
        OperationId = "GetFoodStoreComments",
        Tags = new []{"FoodStores"}
    )]
    public async Task<IEnumerable<CommentResource>> GetAllByFoodStoreIdAsync(int foodStoreId)
    {
        var comments = await _commentService.ListByFoodStoreIdAsync(foodStoreId);
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
}