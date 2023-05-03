using System.Text.Json.Serialization;
using TastyPoint.API.Profiles.Domain.Models;

namespace TastyPoint.API.Security.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    
    [JsonIgnore]
    public string PasswordHash { get; set; }
    
    //Relationships
    public UserProfile UserProfile { get; set; }
}