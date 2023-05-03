using System.Net.Mime;
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
[Produces(MediaTypeNames.Application.Json)]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Products",
        Description = "Get all the existing products",
        OperationId = "GetProduct",
        Tags = new []{"Products"}
    )]
    public async Task<IEnumerable<ProductResource>> GetAllAsync()
    {
        var products = await _productService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

        return resources;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Product by Id",
        Description = "Get existing products with specific Id",
        OperationId = "GetProductById",
        Tags = new []{"Products"}
    )]
    public async Task<ProductResource> GetByIdAsync(int id)
    {
        var product = await _productService.FindByIdAsync(id);
        var resource = _mapper.Map<Product, ProductResource>(product.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Product",
        Description = "Add new product in the database",
        OperationId = "PostProduct",
        Tags = new []{"Products"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var product = _mapper.Map<SaveProductResource, Product>(resource);

        var result = await _productService.SaveAsync(product);

        if (!result.Success)
            return BadRequest(result.Message);
        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(productResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Product",
        Description = "Update some existing product by Id",
        OperationId = "PutProduct",
        Tags = new []{"Products"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var tutorial = _mapper.Map<SaveProductResource, Product>(resource);

        var result = await _productService.UpdateAsync(id, tutorial);

        if (!result.Success)
            return BadRequest(result.Message);

        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(productResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Product",
        Description = "Delete some existing product by Id",
        OperationId = "DeleteProduct",
        Tags = new []{"Products"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(productResource);
    }
}