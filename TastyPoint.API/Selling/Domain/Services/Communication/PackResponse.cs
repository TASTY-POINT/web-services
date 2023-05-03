using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Shared.Services.Communication;

namespace TastyPoint.API.Selling.Domain.Services.Communication;

public class PackResponse: BaseResponse<Pack>
{
    public PackResponse(string message) : base(message)
    {
    }

    public PackResponse(Pack resource) : base(resource)
    {
    }
}