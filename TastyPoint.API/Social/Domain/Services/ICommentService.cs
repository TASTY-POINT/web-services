using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Services.Communication;

namespace TastyPoint.API.Social.Domain.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> ListAsync();
    Task<IEnumerable<Comment>> ListByFoodStoreIdAsync(int foodStoreId);
    Task<CommentResponse> FindByIdAsync(int commentId);
    Task<CommentResponse> SaveAsync(Comment comment);
    Task<CommentResponse> UpdateAsync(int commentId, Comment comment);
    Task<CommentResponse> DeleteAsync(int commentId);
}