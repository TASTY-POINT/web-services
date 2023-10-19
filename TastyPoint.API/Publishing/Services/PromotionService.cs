using TastyPoint.API.Profiles.Domain.Repositories;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Domain.Repositories;
using TastyPoint.API.Publishing.Domain.Services;
using TastyPoint.API.Publishing.Domain.Services.Communication;
using TastyPoint.API.Selling.Domain.Repositories;
using TastyPoint.API.Shared.Domain.Repositories;

namespace TastyPoint.API.Publishing.Services;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileRepository _userProfileRepository;

    public PromotionService(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork, IPackRepository packRepository, IUserProfileRepository userProfileRepository)
    {
        _promotionRepository = promotionRepository;
        _unitOfWork = unitOfWork;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<IEnumerable<Promotion>> ListAsync()
    {
        return await _promotionRepository.ListAsync();
    }

    public async Task<PromotionResponse> FindByIdAsync(int promotionId)
    {
        var existingPromotion = await _promotionRepository.FindByIdAsync(promotionId);
        
        if (existingPromotion == null)
            return new PromotionResponse("This promotion doesn't exist or is sold out.");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new PromotionResponse(existingPromotion);
        }
        catch (Exception e)
        {
            return new PromotionResponse($"An error occurred while searching the promotion: {e.Message}");
        }
    }

    public async Task<IEnumerable<Promotion>> ListByUserProfileIdAsync(int userProfileId)
    {
        return await _promotionRepository.FindByUserProfileIdAsync(userProfileId);
    }

    public async Task<PromotionResponse> SaveAsync(Promotion promotion)
    {
        try
        {
            await _promotionRepository.AddAsync(promotion);
            await _unitOfWork.CompleteAsync();
            return new PromotionResponse(promotion);
        }
        catch (Exception e)
        {
            return new PromotionResponse($"An error occurred while saving the promotion: {e.Message}");
        }
    }

    public async Task<PromotionResponse> UpdateAsync(int promotionId, Promotion promotion)
    {
        var existingPromotion = await _promotionRepository.FindByIdAsync(promotionId);

        if (existingPromotion == null)
            return new PromotionResponse("This promotion doesn't exist or is sold out.");
        
        var existingUserProfile = await _userProfileRepository.FindByIdAsync(promotion.UserProfileId);
        
        if (existingUserProfile == null)
            return new PromotionResponse("Invalid Pack");

        existingPromotion.Title = promotion.Title;
        existingPromotion.SubTitle = promotion.SubTitle;
        existingPromotion.Description = promotion.Description;
        existingPromotion.Quantity = promotion.Quantity;
        existingPromotion.ExpirationDate = promotion.ExpirationDate;
        existingPromotion.EntryDate = promotion.EntryDate;
        existingPromotion.Image = promotion.Image;

        try
        {
            _promotionRepository.Update(existingPromotion);
            await _unitOfWork.CompleteAsync();

            return new PromotionResponse(existingPromotion);
        }
        catch (Exception e)
        {
            return new PromotionResponse($"An error occurred while updating the promotion: {e.Message}");
        }
    }

    public async Task<PromotionResponse> DeleteAsync(int id)
    {
        var existingPromotion = await _promotionRepository.FindByIdAsync(id);
        if (existingPromotion == null)
            return new PromotionResponse("This promotion doesn't exist or is sold out.");

        try
        {
            _promotionRepository.Remove(existingPromotion);
            await _unitOfWork.CompleteAsync();

            return new PromotionResponse(existingPromotion);
        }
        catch (Exception e)
        {
            return new PromotionResponse($"An error occurred while deleting the promotion: {e.Message}");
        }
    }
}