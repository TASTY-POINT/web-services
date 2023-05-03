using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Shared.Extensions;
using TastyPoint.API.Subscription.Domain.Models;
using TastyPoint.API.Subscription.Domain.Services;
using TastyPoint.API.Subscription.Resources;

namespace TastyPoint.API.Subscription.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class BusinessPlanController : ControllerBase
{
    private readonly IBusinessPlanService _businessPlanService;
    private readonly IMapper _mapper;

    public BusinessPlanController(IBusinessPlanService businessPlanService, IMapper mapper)
    {
        _businessPlanService = businessPlanService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Business Plan",
        Description = "Get all the existing Business Plan",
        OperationId = "GetBusinessPlans",
        Tags = new []{"BusinessPlans"}
    )]
    public async Task<IEnumerable<BusinessPlanResource>> GetAllAsync()
    {
        var businessPlan = await _businessPlanService.ListAsync();
        var resources = _mapper.Map<IEnumerable<BusinessPlan>, IEnumerable<BusinessPlanResource>>(businessPlan);

        return resources;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Business Plan by Id",
        Description = "Get existing Business Plan with specific Id",
        OperationId = "GetBusinessPlanById",
        Tags = new[] { "BusinessPlans" }
    )]
    public async Task<BusinessPlanResource> GetByIdAsync(int id)
    {
        var businessPlan = await _businessPlanService.FindByIdAsync(id);
        var resource = _mapper.Map<BusinessPlan, BusinessPlanResource>(businessPlan.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Business Plan",
        Description = "Add new Business Plan in the database",
        OperationId = "PostBusiness Plan",
        Tags = new []{"BusinessPlans"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveBusinessPlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var businessPlan = _mapper.Map<SaveBusinessPlanResource, BusinessPlan>(resource);

        var result = await _businessPlanService.SaveAsync(businessPlan);

        if (!result.Success)
            return BadRequest(result.Message);

        var businessPlanResource = _mapper.Map<BusinessPlan, BusinessPlanResource>(result.Resource);

        return Ok(businessPlanResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Business Plan",
        Description = "Update some existing Business Plan by Id",
        OperationId = "PutBusiness Plan",
        Tags = new []{"BusinessPlans"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBusinessPlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var businessPlan = _mapper.Map<SaveBusinessPlanResource, BusinessPlan>(resource);
        var result = await _businessPlanService.UpdateAsync(id, businessPlan);

        if (!result.Success)
            return BadRequest(result.Message);

        var businessPlanResource = _mapper.Map<BusinessPlan, BusinessPlanResource>(result.Resource);

        return Ok(businessPlanResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Business Plan",
        Description = "Delete some existing Business Plan by Id",
        OperationId = "DeleteBusiness Plan",
        Tags = new []{"BusinessPlans"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _businessPlanService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var businessPlanResource = _mapper.Map<BusinessPlan, BusinessPlanResource>(result.Resource);

        return Ok(businessPlanResource);
    }
}