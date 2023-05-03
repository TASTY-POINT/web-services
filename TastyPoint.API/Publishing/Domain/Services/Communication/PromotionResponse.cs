using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Shared.Services.Communication;

namespace TastyPoint.API.Publishing.Domain.Services.Communication;

public class PromotionResponse : BaseResponse<Promotion>
{
    public PromotionResponse(string message) : base(message)
    {
    }

    public PromotionResponse(Promotion resource) : base(resource)
    {
    }
}