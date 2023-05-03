using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Domain.Services;
using TastyPoint.API.Publishing.Resources;
using TastyPoint.API.Shared.Extensions;

namespace TastyPoint.API.Publishing.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PromotionController: ControllerBase
{
    private readonly IPromotionService _promotionService;
    private readonly IMapper _mapper;


    public PromotionController(IPromotionService promotionService, IMapper mapper)
    {
        _promotionService = promotionService;
        _mapper = mapper;
        
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Promotions",
        Description = "Get all the existing Promotions",
        OperationId = "GetPromotion",
        Tags = new []{"Promotions"}
    )]
    public async Task<IEnumerable<PromotionResource>> GetAllAsync()
    {
        var promotions = await _promotionService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Promotion>, IEnumerable<PromotionResource>>(promotions);
        return resources;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Promotion by Id",
        Description = "Get existing Promotions with specific Id",
        OperationId = "GetPromotionById",
        Tags = new []{"Promotions"}
    )]
    public async Task<PromotionResource> GetByIdAsync(int id)
    {
        var promotion = await _promotionService.FindByIdAsync(id);
        var resource = _mapper.Map<Promotion, PromotionResource>(promotion.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Promotion",
        Description = "Add new Promotion in the database",
        OperationId = "PostPromotion",
        Tags = new []{"Promotions"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SavePromotionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var promotion = _mapper.Map<SavePromotionResource, Promotion>(resource);

        var result = await _promotionService.SaveAsync(promotion);

        if (!result.Success)
            return BadRequest(result.Message);

        var promotionResource = _mapper.Map<Promotion,PromotionResource>(result.Resource);

        return Ok(promotionResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Promotion",
        Description = "Update some existing Promotion by Id",
        OperationId = "PutPromotion",
        Tags = new []{"Promotions"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePromotionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var promotion = _mapper.Map<SavePromotionResource, Promotion>(resource);

        var result = await _promotionService.UpdateAsync(id, promotion);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var promotionResource = _mapper.Map<Promotion, PromotionResource>(result.Resource);

        return Ok(promotionResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Promotion",
        Description = "Delete some existing Promotion by Id",
        OperationId = "DeletePromotion",
        Tags = new []{"Promotions"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _promotionService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var promotionResource = _mapper.Map<Promotion, PromotionResource>(result.Resource);

        return Ok(promotionResource);
    }
    
}