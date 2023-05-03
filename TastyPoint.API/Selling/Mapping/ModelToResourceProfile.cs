using AutoMapper;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Resources;

namespace TastyPoint.API.Selling.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Pack, PackResource>();
        CreateMap<Product, ProductResource>();
    }
}