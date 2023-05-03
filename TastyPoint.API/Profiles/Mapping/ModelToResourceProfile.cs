using AutoMapper;
using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Profiles.Resources;

namespace TastyPoint.API.Profiles.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<UserProfile, UserProfileResource>();
    }
}