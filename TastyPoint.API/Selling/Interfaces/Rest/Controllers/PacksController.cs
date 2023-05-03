using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Services;
using TastyPoint.API.Selling.Resources;
using TastyPoint.API.Shared.Extensions;

namespace TastyPoint.API.Selling.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PacksController:ControllerBase
{
    private readonly IPackService _packService;
    private readonly IMapper _mapper;

    public PacksController(IPackService packService, IMapper mapper)
    {
        _packService = packService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Packs",
        Description = "Get all the existing packs",
        OperationId = "GetPack",
        Tags = new []{"Packs"}
    )]
    public async Task<IEnumerable<PackResource>> GetAllAsync()
    {
        var packs = await _packService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Pack>, IEnumerable<PackResource>>(packs);
        return resources;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Pack by Id",
        Description = "Get existing pack with specific Id",
        OperationId = "GetPackById",
        Tags = new []{"Packs"}
    )]
    public async Task<PackResource> GetByIdAsync(int id)
    {
        var pack = await _packService.FindByIdAsync(id);
        var resource = _mapper.Map<Pack, PackResource>(pack.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Pack",
        Description = "Add new pack in the database",
        OperationId = "PostPack",
        Tags = new []{"Packs"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SavePackResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var pack = _mapper.Map<SavePackResource, Pack>(resource);

        var result = await _packService.SaveAsync(pack);

        if (!result.Success)
            return BadRequest(result.Message);

        var packResource = _mapper.Map<Pack, PackResource>(result.Resource);

        return Created(nameof(PostAsync), packResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Pack",
        Description = "Update some existing pack by Id",
        OperationId = "PutPack",
        Tags = new []{"Packs"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePackResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var pack = _mapper.Map<SavePackResource, Pack>(resource);

        var result = await _packService.UpdateAsync(id, pack);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var packResource = _mapper.Map<Pack, PackResource>(result.Resource);

        return Ok(packResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Pack",
        Description = "Delete some existing pack by Id",
        OperationId = "DeletePack",
        Tags = new []{"Packs"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _packService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var packResource = _mapper.Map<Pack, PackResource>(result.Resource);

        return Ok(packResource);
    }
}