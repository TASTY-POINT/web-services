using TastyPoint.API.Shared.Domain.Repositories;
using TastyPoint.API.Social.Domain.Models;
using TastyPoint.API.Social.Domain.Repositories;
using TastyPoint.API.Social.Domain.Services;
using TastyPoint.API.Social.Domain.Services.Communication;

namespace TastyPoint.API.Social.Services;

public class FoodStoreService: IFoodStoreService
{
    private readonly IFoodStoreRepository _foodStoreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FoodStoreService(IFoodStoreRepository foodStoreRepository, IUnitOfWork unitOfWork)
    {
        _foodStoreRepository = foodStoreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<FoodStore>> ListAsync()
    {
        return await _foodStoreRepository.ListAsync();
    }

    public async Task<FoodStoreResponse> FindByIdAsync(int foodStoreId)
    {
        var existingFoodStore = await _foodStoreRepository.FindByIdAsync(foodStoreId);
        if (existingFoodStore == null)
            return new FoodStoreResponse("Food Store not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new FoodStoreResponse(existingFoodStore);
        }
        catch (Exception e)
        {
            return new FoodStoreResponse($"An error occurred while finding the food store: {e.Message}");
        }
    }

    public async Task<FoodStore> FindByUserProfileIdAsync(int userProfileId)
    {
        return await _foodStoreRepository.FindByUserProfileId(userProfileId);
    }

    public async Task<FoodStoreResponse> SaveAsync(FoodStore foodStore)
    {
        try
        {
            await _foodStoreRepository.AddAsync(foodStore);
            await _unitOfWork.CompleteAsync();
            return new FoodStoreResponse(foodStore);
        }
        catch (Exception e)
        {
            return new FoodStoreResponse($"An error occurred while saving the food store: {e.Message}");
        }
    }

    public async Task<FoodStoreResponse> UpdateAsync(int foodStoreId, FoodStore foodStore)
    {
        var existingFoodStore = await _foodStoreRepository.FindByIdAsync(foodStoreId);

        if (existingFoodStore == null)
            return new FoodStoreResponse("Food Store not found");

        existingFoodStore.Name = foodStore.Name;
        existingFoodStore.Address = foodStore.Address;
        existingFoodStore.Description = foodStore.Description;
        existingFoodStore.Image = foodStore.Image;
        existingFoodStore.Favorite = foodStore.Favorite;
        existingFoodStore.Rate = foodStore.Rate;

        try
        {
            _foodStoreRepository.Update(existingFoodStore);
            await _unitOfWork.CompleteAsync();

            return new FoodStoreResponse(existingFoodStore);
        }
        catch (Exception e)
        {
            return new FoodStoreResponse($"An error occurred while updating the food store: {e.Message}");
        }
    }

    public async Task<FoodStoreResponse> DeleteAsync(int id)
    {
        var existingFoodStore = await _foodStoreRepository.FindByIdAsync(id);

        if (existingFoodStore == null)
            return new FoodStoreResponse("Food Store not found");

        try
        {
            _foodStoreRepository.Remove(existingFoodStore);
            await _unitOfWork.CompleteAsync();

            return new FoodStoreResponse(existingFoodStore);
        }
        catch (Exception e)
        {
            return new FoodStoreResponse($"An error occurred while updating the food store: {e.Message}");
        }
    }
}