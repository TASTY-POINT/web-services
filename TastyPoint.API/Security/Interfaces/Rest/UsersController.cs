using AutoMapper;
using TastyPoint.API.Security.Authorization.Attributes;
//using Microsoft.AspNetCore.Authorization;
using TastyPoint.API.Security.Domain.Models;
using TastyPoint.API.Security.Domain.Services;
using TastyPoint.API.Security.Domain.Services.Communication;
using TastyPoint.API.Security.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TastyPoint.API.Security.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign In",
        Description = "User sign in the application",
        OperationId = "SignIn",
        Tags = new []{"Users"}
    )]
    public async Task<ActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign Up",
        Description = "User sign up the application",
        OperationId = "SignUp",
        Tags = new []{"Users"}
    )]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Users",
        Description = "Get all the existing users",
        OperationId = "GetUsers",
        Tags = new []{"Users"}
    )]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }
        
}