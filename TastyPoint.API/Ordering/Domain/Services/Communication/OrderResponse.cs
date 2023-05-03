using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Shared.Services.Communication;

namespace TastyPoint.API.Ordering.Domain.Services.Communication;

public class OrderResponse: BaseResponse<Order>
{
        public OrderResponse(string message) : base(message)
        {
        }

        public OrderResponse(Order resource) : base(resource)
        {
        }
}
