using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Profiles.Domain.Services;
using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Profiles.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/user/{userId}/userprofile")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProfileUserController: ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;

    public UserProfileUserController(IUserProfileService userProfileService, IMapper mapper)
    {
        _userProfileService = userProfileService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get User Profile for given User",
        Description = "Get existing User Profile associated with specified User",
        OperationId = "GetUserProfileUser",
        Tags = new []{"UserProfiles"}
    )]
    public async Task<UserProfileResource> GetUserProfileByUserId(int userId)
    {
        var userProfile = await _userProfileService.FindByUserIdAsync(userId);
        var resource = _mapper.Map<UserProfile, UserProfileResource>(userProfile);
        return resource;
    }
}