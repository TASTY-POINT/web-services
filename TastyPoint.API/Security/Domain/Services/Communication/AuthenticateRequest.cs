using System.ComponentModel.DataAnnotations;

namespace TastyPoint.API.Security.Domain.Services.Communication;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}