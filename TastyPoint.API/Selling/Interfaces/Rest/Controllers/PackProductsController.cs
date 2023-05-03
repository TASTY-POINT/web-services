using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Services;
using TastyPoint.API.Selling.Resources;

namespace TastyPoint.API.Selling.Interfaces.Rest.Controllers;

[ApiController]
[Route("/api/v1/packs/{packId}/products")]
[Produces(MediaTypeNames.Application.Json)]
public class PackProductsController: ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public PackProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Products for given Pack",
        Description = "Get existing Products associated with specified Pack",
        OperationId = "GetPackProducts",
        Tags = new []{"Packs"}
    )]
    public async Task<IEnumerable<ProductResource>> GetAllByPackIdAsync(int packId)
    {
        var products = await _productService.ListByPackIdAsync(packId);

        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
        return resources;
    }
    
}