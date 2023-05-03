using AutoMapper;
using TastyPoint.API.Subscription.Domain.Models;
using TastyPoint.API.Subscription.Resources;

namespace TastyPoint.API.Subscription.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveBusinessPlanResource, BusinessPlan>();
    }
}