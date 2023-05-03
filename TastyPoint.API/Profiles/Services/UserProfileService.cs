using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Profiles.Domain.Repositories;
using TastyPoint.API.Profiles.Domain.Services;
using TastyPoint.API.Profiles.Domain.Services.Communication;
using TastyPoint.API.Shared.Domain.Repositories;

namespace TastyPoint.API.Profiles.Services;

public class UserProfileService: IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileService(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserProfile>> ListAsync()
    {
        return await _userProfileRepository.ListAsync();
    }

    public async Task<UserProfileResponse> FindByIdAsync(int userProfileId)
    {
        var existingUserProfile = await _userProfileRepository.FindByIdAsync(userProfileId);
        if (existingUserProfile == null)
            return new UserProfileResponse("User Profile not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new UserProfileResponse(existingUserProfile);
        }
        catch (Exception e)
        {
            return new UserProfileResponse($"An error occurred while finding the user profile: {e.Message}");
        }
    }

    public async Task<UserProfile> FindByUserIdAsync(int userId)
    {
        return await _userProfileRepository.FindByUserIdAsync(userId);
    }


    public async Task<UserProfileResponse> SaveAsync(UserProfile userProfile)
    {
        try
        {
            await _userProfileRepository.AddAsync(userProfile);
            await _unitOfWork.CompleteAsync();
            return new UserProfileResponse(userProfile);
        }
        catch (Exception e)
        {
            return new UserProfileResponse($"An error occurred while saving the user profile: {e.Message}");
        }
    }

    public async Task<UserProfileResponse> UpdateAsync(int userProfileId, UserProfile userProfile)
    {
        var existingUserProfile = await _userProfileRepository.FindByIdAsync(userProfileId);

        if (existingUserProfile == null)
            return new UserProfileResponse("User Profile not found");
        existingUserProfile.Name = userProfile.Name;
        existingUserProfile.PhoneNumber = userProfile.PhoneNumber;

        try
        {
            _userProfileRepository.Update(existingUserProfile);
            await _unitOfWork.CompleteAsync();

            return new UserProfileResponse(existingUserProfile);
        }
        catch (Exception e)
        {
            return new UserProfileResponse($"An error occurred while updating the user profile: {e.Message}");
        }
    }

    public async Task<UserProfileResponse> DeleteAsync(int id)
    {
        var existingUserProfile = await _userProfileRepository.FindByIdAsync(id);

        if (existingUserProfile == null)
            return new UserProfileResponse("User Profile not found");

        try
        {
            _userProfileRepository.Remove(existingUserProfile);
            await _unitOfWork.CompleteAsync();

            return new UserProfileResponse(existingUserProfile);
        }
        catch (Exception e)
        {
            return new UserProfileResponse($"An error occurred while deleting the user profile: {e.Message}");
        }
    }
}