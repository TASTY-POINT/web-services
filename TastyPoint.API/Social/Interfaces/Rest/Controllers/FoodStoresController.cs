using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Shared.Extensions;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Services;
using TastyPoint.API.Social.Resources;

namespace TastyPoint.API.Social.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class FoodStoresController: ControllerBase
{
    private readonly IFoodStoreService _foodStoreService;
    private readonly IMapper _mapper;

    public FoodStoresController(IFoodStoreService foodStoreService, IMapper mapper)
    {
        _foodStoreService = foodStoreService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Food Stores",
        Description = "Get all the existing Food Stores",
        OperationId = "GetFoodStore",
        Tags = new []{"FoodStores"}
    )]
    public async Task<IEnumerable<FoodStoreResource>> GetAllAsync()
    {
        var foodStores = await _foodStoreService.ListAsync();
        var resources = _mapper.Map<IEnumerable<FoodStore>, IEnumerable<FoodStoreResource>>(foodStores);
        return resources;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Food Store by Id",
        Description = "Get existing Food Store with specific Id",
        OperationId = "GetFoodStoreById",
        Tags = new []{"FoodStores"}
    )]
    public async Task<FoodStoreResource> GetByIdAsync(int id)
    {
        var foodStore = await _foodStoreService.FindByIdAsync(id);
        var resource = _mapper.Map<FoodStore, FoodStoreResource>(foodStore.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Food Store",
        Description = "Add new Food Store in the database",
        OperationId = "PostFoodStore",
        Tags = new []{"FoodStores"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveFoodStoreResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var foodStore = _mapper.Map<SaveFoodStoreResource, FoodStore>(resource);

        var result = await _foodStoreService.SaveAsync(foodStore);

        if (!result.Success)
            return BadRequest(result.Message);

        var foodStoreResource = _mapper.Map<FoodStore, FoodStoreResource>(result.Resource);

        return Created(nameof(PostAsync), foodStoreResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Food Store",
        Description = "Update some existing Food Store by Id",
        OperationId = "PutFoodStore",
        Tags = new []{"FoodStores"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFoodStoreResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var foodStore = _mapper.Map<SaveFoodStoreResource, FoodStore>(resource);

        var result = await _foodStoreService.UpdateAsync(id, foodStore);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var foodStoreResource = _mapper.Map<FoodStore, FoodStoreResource>(result.Resource);

        return Ok(foodStoreResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Food Store",
        Description = "Delete some existing Food Store by Id",
        OperationId = "DeleteFoodStore",
        Tags = new []{"FoodStores"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _foodStoreService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var foodStoreResource = _mapper.Map<FoodStore, FoodStoreResource>(result.Resource);

        return Ok(foodStoreResource);
    }
}