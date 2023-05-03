using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Services;
using TastyPoint.API.Social.Resources;

namespace TastyPoint.API.Social.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/userprofile/{userProfileId}/food-store")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProfileFoodStoreController
{
    private readonly IFoodStoreService _foodStoreService;
    private readonly IMapper _mapper;

    public UserProfileFoodStoreController(IFoodStoreService foodStoreService, IMapper mapper)
    {
        _foodStoreService = foodStoreService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Food Store for given User Profile",
        Description = "Get existing Food Store associated with specified User Profile",
        OperationId = "GetFoodStoreUserProfile",
        Tags = new[] { "FoodStores" }
    )]
    public async Task<FoodStoreResource> GetFoodStoreByUserProfileId(int userProfileId)
    {
        var foodStore = await _foodStoreService.FindByUserProfileIdAsync(userProfileId);
        var resource = _mapper.Map<FoodStore, FoodStoreResource>(foodStore);
        return resource;
    }
}