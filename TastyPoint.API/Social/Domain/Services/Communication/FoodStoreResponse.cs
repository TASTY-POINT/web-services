using TastyPoint.API.Shared.Services.Communication;
using TastyPoint.API.Social.Domain.Models;

namespace TastyPoint.API.Social.Domain.Services.Communication;

public class FoodStoreResponse: BaseResponse<FoodStore>
{
    public FoodStoreResponse(string message) : base(message)
    {
    }

    public FoodStoreResponse(FoodStore resource) : base(resource)
    {
    }
}