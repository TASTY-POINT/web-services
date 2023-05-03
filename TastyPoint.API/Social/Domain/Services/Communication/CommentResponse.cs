using TastyPoint.API.Shared.Services.Communication;
using TastyPoint.API.Social.Domain.Models;

namespace TastyPoint.API.Social.Domain.Services.Communication;

public class CommentResponse: BaseResponse<Comment>
{
    public CommentResponse(string message) : base(message)
    {
    }

    public CommentResponse(Comment resource) : base(resource)
    {
    }
}