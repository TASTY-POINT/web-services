using TastyPoint.API.Social.Domain.Models;

namespace TastyPoint.API.Social.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int commentId);
    Task<IEnumerable<Comment>> FindByFoodStoreAsync(int foodStoreId);
    void Update(Comment comment);
    void Remove(Comment comment);
}