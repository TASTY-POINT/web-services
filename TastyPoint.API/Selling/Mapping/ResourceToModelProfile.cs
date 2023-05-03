using AutoMapper;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Resources;

namespace TastyPoint.API.Selling.Mapping;

public class ResourceToModelProfile:Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePackResource, Pack>();
        CreateMap<SaveProductResource, Product>();
    }
}