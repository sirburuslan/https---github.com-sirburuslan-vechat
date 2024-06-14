/*
 * @class Users Create Repository Extension
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-19
 *
 * This class is used to create users
 */

// System Utils
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
// Installed Utils
using MongoDB.Driver;
// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Administrator.Users;
using api.Models.Dtos.Auth.Users;
using api.Models.Entities.Users;
using api.Utilities;
using api.Utilities.Db;

// Namespace for Users Account repositories
namespace api.Repositories.Users.Account;

/// <summary>
/// Users Create Repository pattern
/// </summary>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="db">Database connection</param>
/// <param name="passwordHasher">PasswordHasher instance</param>
/// <param name="logger">Logger instance</param>
public class CreateRepository(IMemoryCache memoryCache, MongoDb db, IPasswordHasher<UserEntity> passwordHasher, ILogger<CreateRepository> logger)
{
    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="newUserDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    public async Task<ResponseDto<UserDto>> CreateUserAsync(NewUserDto newUserDto)
    {
        try
        {
            // Filter the users emails
            FilterDefinition<UserEntity> filterEmails = Builders<UserEntity>.Filter.Eq(m => m.Email, newUserDto.Email);

            // Search for required email which a new guest uses to join
            UserEntity userEmail = await db.Users.Find(filterEmails).FirstOrDefaultAsync();

            // Verify if the email is already registered
            if (userEmail != null)
            {
                // Return response
                return new ResponseDto<UserDto>
                {
                    Result = null,
                    Message = Words.Get("EmailFound")
                };
            }

            // Init the User entity
            UserEntity userEntity = new()
            {
                // Set the user's First Name
                FirstName = newUserDto.FirstName ?? string.Empty,
                // Set the user's Last Name
                LastName = newUserDto.LastName ?? string.Empty,               
                // Set the user's email
                Email = newUserDto.Email ?? string.Empty
            };

            // Set the user's password
            userEntity.Password = passwordHasher.HashPassword(userEntity, newUserDto.Password ?? string.Empty);

            // Set the user's role
            userEntity.Role = newUserDto.Role;

            // Set the joined time
            userEntity.Created = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Add the entity to the database
            await db.Users.InsertOneAsync(userEntity);

            // Remove the caches for users group
            new Cache(memoryCache).Remove("users");

            // Return response
            return new ResponseDto<UserDto>
            {
                Result = new UserDto
                {
                    UserId = userEntity.UserId
                },
                Message = Words.Get("AccountCreated")
            };

        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while sign up.");

            // Return response
            return new ResponseDto<UserDto>
            {
                Result = null,
                Message = Words.Get("ErrorSignUp")
            };
        }
    }
}