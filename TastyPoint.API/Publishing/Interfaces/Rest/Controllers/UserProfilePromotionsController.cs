using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Domain.Services;
using TastyPoint.API.Publishing.Resources;

namespace TastyPoint.API.Publishing.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/userprofile/{userProfileId}/promotions")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProfilePromotionsController: ControllerBase
{
    private readonly IPromotionService _promotionService;
    private readonly IMapper _mapper;

    public UserProfilePromotionsController(IPromotionService promotionService, IMapper mapper)
    {
        _promotionService = promotionService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Promotions for given User Profile",
        Description = "Get existing Promotions associated with specified User Profile",
        OperationId = "GetUserProfilePromotions",
        Tags = new[] { "Promotions" }
    )]
    public async Task<IEnumerable<PromotionResource>> GetPromotionsByUserProfileId(int userProfileId)
    {
        var promotions = await _promotionService.ListByUserProfileIdAsync(userProfileId);
        var resources = _mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionResource>>(promotions);
        return resources;
    }
}