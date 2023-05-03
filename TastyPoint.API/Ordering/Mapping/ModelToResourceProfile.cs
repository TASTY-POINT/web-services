using AutoMapper;
using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Ordering.Resources;

namespace TastyPoint.API.Ordering.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Order, OrderResource>();
    }
}