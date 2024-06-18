/*
 * @class Users Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-19
 *
 * This class is used to manage the user's data
 */

// System Namespaces
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

// Installed Utils
using MongoDB.Bson;

// App Namespaces
using api.Models.Dtos;
using api.Models.Dtos.Administrator.Users;
using api.Models.Dtos.Auth.Users;
using api.Models.Entities.Users;
using api.Repositories.Users.Account;
using UsersOptions = api.Repositories.Users.Options;
using api.Services.Interfaces.Users;
using api.Utilities.Db;

// Namespace for Users repositories
namespace api.Repositories.Users;

/// <summary>
/// Users Repository pattern
/// </summary>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="db">Database connection</param>
/// <param name="passwordHasher">PasswordHasher instance</param>
/// <param name="logger">Logger instance</param>
public class UsersRepository(IMemoryCache memoryCache, MongoDb db, IPasswordHasher<UserEntity> passwordHasher, ILogger<CreateRepository> logger) : IUsersRepository
{
    /// <summary>
    /// Register a user
    /// </summary>
    /// <param name="registrationDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    public async Task<ResponseDto<UserDto>> RegisterUserAsync(RegistrationDto registrationDto)
    {
        // Init Auth Repository
        AuthRepository authRepository = new(memoryCache, db, passwordHasher, logger);

        // Register user and return the response
        return await authRepository.RegisterUserAsync(registrationDto);
    }
    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="newUserDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    public async Task<ResponseDto<UserDto>> CreateUserAsync(NewUserDto newUserDto)
    {
        // Init Create Repository
        CreateRepository createRepository = new(memoryCache, db, passwordHasher, logger);

        // Create a user and return the response
        return await createRepository.CreateUserAsync(newUserDto);
    }

    /// <summary>
    /// Save bulk options for Users
    /// </summary>
    /// <param name="optionsList">Users options list</param>
    /// <returns>Boolean response</returns>
    public async Task<bool> SaveOptionsAsync(List<UserOptionDto> optionsList)
    {

        // Init Create Repository
        UsersOptions.CreateRepository createRepository = new(db, memoryCache, logger);

        // Save bulk options and return the response
        return await createRepository.SaveOptionsAsync(optionsList);

    }

    /// <summary>
    /// Update bulk options
    /// </summary>
    /// <param name="optionsList">Users options list</param>
    /// <returns>Boolean response</returns>
    public async Task<bool> UpdateOptionsAsync(List<UserOptionDto> optionsList)
    {

        // Init Update Repository
        UsersOptions.UpdateRepository updateRepository = new(db, memoryCache, logger);

        // Update bulk options and return the response
        return await updateRepository.UpdateOptionsAsync(optionsList);

    }

    /// <summary>
    /// Check if the user and password is correct
    /// </summary>
    /// <param name="signInDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    public async Task<ResponseDto<UserDto>> SignIn(SignInDto signInDto)
    {
        // Init Auth Repository
        AuthRepository authRepository = new(memoryCache, db, passwordHasher, logger);

        // Authentificate user and return the response
        return await authRepository.SignIn(signInDto);
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <param name="searchDto">Search parameters</param>
    /// <returns>List with users</returns>
    public async Task<ResponseDto<ElementsDto<UserDto>>> GetUsersAsync(SearchDto searchDto) {

        // Init Read Repository
        ReadRepository readRepository = new(db, memoryCache, logger);

        // Search for users
        return await readRepository.GetUsersAsync(searchDto);

    }

    /// <summary>
    /// Get user data
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>UserDto with user's data</returns>
    public async Task<ResponseDto<UserDto>> GetUserAsync(ObjectId userId)
    {
        // Init Read Repository
        ReadRepository readRepository = new(db, memoryCache, logger);

        // Get user by id and return the response
        return await readRepository.GetUserAsync(userId);
    }

    /// <summary>
    /// Get the options list
    /// </summary>
    /// <param name="userId">User's ID</param>
    /// <returns>Get the options list</returns>
    public async Task<ResponseDto<List<UserOptionDto>>> OptionsListAsync(ObjectId userId)
    {

        // Init Read Repository
        UsersOptions.ReadRepository readRepository = new(db, memoryCache, logger);

        // Read options and return the response
        return await readRepository.OptionsListAsync(userId);

    }

}