/*
 * @interface Users Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-13
 *
 * This interface is implemented in UsersRepository
 */

// Installed Utils
using MongoDB.Bson;

// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Administrator.Users;
using api.Models.Dtos.Auth.Users;

// Namespace for Users Repositories
namespace api.Services.Interfaces.Users;

/// <summary>
/// Users Interface
/// </summary>
public interface IUsersRepository
{
    /// <summary>
    /// Register a user
    /// </summary>
    /// <param name="registrationDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    Task<ResponseDto<UserDto>> RegisterUserAsync(RegistrationDto registrationDto);
    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="newUserDto">RegistrationDto dto with the user's data</param>
    /// <returns>Response with user data</returns>
    Task<ResponseDto<UserDto>> CreateUserAsync(NewUserDto newUserDto);

    /// <summary>
    /// Save bulk options for Users
    /// </summary>
    /// <param name="optionsList">Users options list</param>
    /// <returns>Boolean response</returns>
    Task<bool> SaveOptionsAsync(List<UserOptionDto> optionsList);

    /// <summary>
    /// Update bulk options
    /// </summary>
    /// <param name="optionsList">Users options list</param>
    /// <returns>Boolean response</returns>
    Task<bool> UpdateOptionsAsync(List<UserOptionDto> optionsList);

    /// <summary>
    /// Check if the user and password is correct
    /// </summary>
    /// <param name="signInDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    Task<ResponseDto<UserDto>> SignIn(SignInDto signInDto);

    /// <summary>
    /// Get user data
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>UserDto with user's data</returns>
    Task<ResponseDto<UserDto>> GetUserAsync(ObjectId userId);

    /// <summary>
    /// Get the options list
    /// </summary>
    /// <param name="userId">User's ID</param>
    /// <returns>Get the options list</returns>
    Task<ResponseDto<List<UserOptionDto>>> OptionsListAsync(ObjectId userId);
}