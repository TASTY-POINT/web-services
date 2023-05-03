using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Repositories;
using TastyPoint.API.Selling.Domain.Services;
using TastyPoint.API.Selling.Domain.Services.Communication;
using TastyPoint.API.Shared.Domain.Repositories;

namespace TastyPoint.API.Selling.Services;

public class PackService: IPackService
{
    private readonly IPackRepository _packRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PackService(IPackRepository packRepository, IUnitOfWork unitOfWork)
    {
        _packRepository = packRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Pack>> ListAsync()
    {
        return await _packRepository.ListAsync();
    }

    public async Task<IEnumerable<Pack>> ListByUserProfileIdAsync(int userProfileId)
    {
        return await _packRepository.FindByUserProfileIdAsync(userProfileId);
    }

    public async Task<PackResponse> FindByIdAsync(int packId)
    {
        var existingPack = await _packRepository.FindByIdAsync(packId);
        
        if (existingPack == null)
            return new PackResponse("Pack not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new PackResponse(existingPack);
        }
        catch (Exception e)
        {
            return new PackResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<PackResponse> SaveAsync(Pack pack)
    {
        try
        {
            await _packRepository.AddAsync(pack);
            await _unitOfWork.CompleteAsync();
            return new PackResponse(pack);
        }
        catch (Exception e)
        {
            return new PackResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<PackResponse> UpdateAsync(int packId, Pack pack)
    {
        var existingPack = await _packRepository.FindByIdAsync(packId);

        if (existingPack == null)
            return new PackResponse("Pack not found");

        existingPack.Name = pack.Name;
        existingPack.Price = pack.Price;

        try
        {
            _packRepository.Update(existingPack);
            await _unitOfWork.CompleteAsync();

            return new PackResponse(existingPack);
        }
        catch (Exception e)
        {
            return new PackResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<PackResponse> DeleteAsync(int id)
    {
        var existingPack = await _packRepository.FindByIdAsync(id);
        if (existingPack == null)
            return new PackResponse("Pack not found");

        try
        {
            _packRepository.Remove(existingPack);
            await _unitOfWork.CompleteAsync();

            return new PackResponse(existingPack);
        }
        catch (Exception e)
        {
            return new PackResponse($"An error occurred while saving the category: {e.Message}");
        }
    }
}