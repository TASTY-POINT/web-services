using AutoMapper;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Resources;

namespace TastyPoint.API.Publishing.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Promotion, PromotionResource>();
    }
}