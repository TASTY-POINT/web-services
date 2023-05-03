using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Shared.Services.Communication;

namespace TastyPoint.API.Profiles.Domain.Services.Communication;

public class UserProfileResponse: BaseResponse<UserProfile>
{
    public UserProfileResponse(string message) : base(message)
    {
    }

    public UserProfileResponse(UserProfile resource) : base(resource)
    {
    }
}