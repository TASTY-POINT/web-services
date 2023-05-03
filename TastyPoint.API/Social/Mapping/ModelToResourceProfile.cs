using AutoMapper;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Resources;

namespace TastyPoint.API.Social.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<FoodStore, FoodStoreResource>();
        CreateMap<Comment, CommentResource>();
    }
}