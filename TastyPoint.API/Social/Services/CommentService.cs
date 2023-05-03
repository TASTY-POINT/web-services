using TastyPoint.API.Shared.Domain.Repositories;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Repositories;
using TastyPoint.API.Social.Domain.Services;
using TastyPoint.API.Social.Domain.Services.Communication;

namespace TastyPoint.API.Social.Services;

public class CommentService: ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFoodStoreRepository _foodStoreRepository;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IFoodStoreRepository foodStoreRepository)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _foodStoreRepository = foodStoreRepository;
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _commentRepository.ListAsync();
    }

    public async Task<IEnumerable<Comment>> ListByFoodStoreIdAsync(int foodStoreId)
    {
        return await _commentRepository.FindByFoodStoreAsync(foodStoreId);
    }

    public async Task<CommentResponse> FindByIdAsync(int commentId)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        if (existingComment == null)
            return new CommentResponse("Comment not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while finding the comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> SaveAsync(Comment comment)
    {
        var existingFoodStore = await _foodStoreRepository.FindByIdAsync(comment.FoodStoreId);
        if (existingFoodStore == null)
            return new CommentResponse("Invalid Food Store");
        
        try
        {
            await _commentRepository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            return new CommentResponse(comment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while saving the comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> UpdateAsync(int commentId, Comment comment)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        if (existingComment == null)
            return new CommentResponse("Comment not found");

        var existingFoodStore = await _foodStoreRepository.FindByIdAsync(comment.FoodStoreId);

        if (existingFoodStore == null)
            return new CommentResponse("Invalid Food Store");

        existingComment.Rate = comment.Rate;
        existingComment.Text = comment.Text;

        try
        {
            _commentRepository.Update(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while saving the comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> DeleteAsync(int commentId)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        if (existingComment == null)
            return new CommentResponse("Comment not found");

        try
        {
            _commentRepository.Remove(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while deleting the comment: {e.Message}");
        }
    }
}