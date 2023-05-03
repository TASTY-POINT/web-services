using AutoMapper;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Resources;

namespace TastyPoint.API.Publishing.Mapping;

public class ResourceToModelProfile:Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePromotionResource, Promotion>();
    }
}