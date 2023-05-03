using System.Net.Mime;
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
[Produces(MediaTypeNames.Application.Json)]
public class CommentsController: ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Comments",
        Description = "Get all the existing Comments",
        OperationId = "GetComment",
        Tags = new []{"Comments"}
    )]
    public async Task<IEnumerable<CommentResource>> GetAllAsync()
    {
        var comments = await _commentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);

        return resources;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Comment by Id",
        Description = "Get existing Comments with specific Id",
        OperationId = "GetCommentById",
        Tags = new []{"Comments"}
    )]
    public async Task<CommentResource> GetByIdAsync(int id)
    {
        var comment = await _commentService.FindByIdAsync(id);
        var resource = _mapper.Map<Comment, CommentResource>(comment.Resource);
        return resource;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Comment",
        Description = "Add new Comment in the database",
        OperationId = "PostComment",
        Tags = new []{"Comments"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);

        var result = await _commentService.SaveAsync(comment);

        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

        return Ok(commentResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Put Comment",
        Description = "Update some existing Comment by Id",
        OperationId = "PutComment",
        Tags = new []{"Comments"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var tutorial = _mapper.Map<SaveCommentResource, Comment>(resource);

        var result = await _commentService.UpdateAsync(id, tutorial);

        if (!result.Success)
            return BadRequest(result.Message);

        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

        return Ok(commentResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Comment",
        Description = "Delete some existing Comment by Id",
        OperationId = "DeleteComment",
        Tags = new []{"Comments"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _commentService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

        return Ok(commentResource);
    }
}