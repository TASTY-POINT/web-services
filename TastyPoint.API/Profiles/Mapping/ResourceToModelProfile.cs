using AutoMapper;
using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Profiles.Mapping;

public class ResourceToModelProfile:Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveUserProfileResource, UserProfile>();
    }
}