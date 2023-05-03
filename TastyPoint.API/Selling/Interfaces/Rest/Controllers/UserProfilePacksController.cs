using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Services;
using TastyPoint.API.Selling.Resources;

namespace TastyPoint.API.Selling.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/userprofiles/{userProfileId}/packs")]
public class UserProfilePacksController:ControllerBase
{
    private readonly IPackService _packService;
    private readonly IMapper _mapper;

    public UserProfilePacksController(IPackService packService, IMapper mapper)
    {
        _packService = packService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Packs for given User Profile",
        Description = "Get existing Packs associated with specified User Profile",
        OperationId = "GetUserProfilePacks",
        Tags = new[] { "Packs" }
    )]
    public async Task<IEnumerable<PackResource>> GetAllByUserProfileIdAsync(int userProfileId)
    {
        var packs = await _packService.ListByUserProfileIdAsync(userProfileId);
        var resources = _mapper.Map<IEnumerable<Pack>, IEnumerable<PackResource>>(packs);
        return resources;
    }
}