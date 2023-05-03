using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Shared.Services.Communication;

namespace TastyPoint.API.Selling.Domain.Services.Communication;

public class ProductResponse: BaseResponse<Product>
{
    public ProductResponse(string message) : base(message)
    {
    }

    public ProductResponse(Product resource) : base(resource)
    {
    }
}