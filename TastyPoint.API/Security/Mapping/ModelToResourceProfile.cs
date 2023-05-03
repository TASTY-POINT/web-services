using AutoMapper;
using TastyPoint.API.Security.Domain.Models;
using TastyPoint.API.Security.Domain.Services.Communication;
using TastyPoint.API.Security.Resources;

namespace TastyPoint.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}