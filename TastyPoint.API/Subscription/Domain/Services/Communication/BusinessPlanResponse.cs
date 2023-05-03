using TastyPoint.API.Shared.Services.Communication;
using TastyPoint.API.Subscription.Domain.Models;

namespace TastyPoint.API.Subscription.Domain.Services.Communication;

public class BusinessPlanResponse : BaseResponse<BusinessPlan>
{
    public BusinessPlanResponse(string message) : base(message)
    {
    }

    public BusinessPlanResponse(BusinessPlan resource) : base(resource)
    {
    }
}