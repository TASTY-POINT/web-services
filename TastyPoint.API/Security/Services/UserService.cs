﻿using AutoMapper;
using TastyPoint.API.Security.Authorization.Handlers.Interfaces;
using TastyPoint.API.Security.Domain.Models;
using TastyPoint.API.Security.Domain.Repositories;
using TastyPoint.API.Security.Domain.Services;
using TastyPoint.API.Security.Domain.Services.Communication;
using TastyPoint.API.Security.Exceptions;
using TastyPoint.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TastyPoint.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }


    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        // Find Username
        var user = await _userRepository.FindByUsernameAsync(model.Username);
        
        // Validate Password
        if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            // On Error throw Exception
            throw new AppException("Username or password is incorrect");

        // On Authentication successful 
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user);
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest model)
    {
        // Validate
        if (_userRepository.ExistsByUsername(model.Username))
            throw new AppException($"Username '{model.Username}' is already taken");

        // Map model to new usr object
        var user = _mapper.Map<User>(model);

        // Hash password
        user.PasswordHash = BCryptNet.HashPassword(model.Password);

        // Save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest model)
    {
        var user = GetById(id);
        
        // Validate
        var userWithExistingName = await _userRepository.FindByUsernameAsync(model.Username);
        if (userWithExistingName != null && !userWithExistingName.Id.Equals(user.Id))
            throw new AppException($"Username '{model.Username}' is already taken");

        // Hash password if it has been entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

        // Transfer model to user and save
        _mapper.Map(model, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
        

    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    // Helper methods
    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}