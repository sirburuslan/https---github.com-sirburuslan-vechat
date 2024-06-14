/*
 * @class Users Authentification Repository
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @authd 2024-05-06
 *
 * This class is used to sign in
 */

// System Utils
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
// Installed Utils
using MongoDB.Driver;
// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Auth.Users;
using api.Models.Entities.Users;
using api.Utilities;
using api.Utilities.Db;

// Namespace for Users Account repositories
namespace api.Repositories.Users.Account;

/// <summary>
/// Users Auth Repository pattern
/// </summary>
/// <param name="memoryCache">Usery cache instance</param>
/// <param name="db">Database connection</param>
/// <param name="passwordHasher">PasswordHasher instance</param>
/// <param name="logger">Represents a type used to perform logging.</param>
public class AuthRepository(IMemoryCache memoryCache, MongoDb db, IPasswordHasher<UserEntity> passwordHasher, ILogger<CreateRepository> logger)
{
    /// <summary>
    /// Register a user
    /// </summary>
    /// <param name="registrationDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    public async Task<ResponseDto<UserDto>> RegisterUserAsync(RegistrationDto registrationDto)
    {
        try
        {
            // Filter the users emails
            FilterDefinition<UserEntity> filterEmails = Builders<UserEntity>.Filter.Eq(m => m.Email, registrationDto.Email);

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
                // Set the user's email
                Email = registrationDto.Email ?? string.Empty
            };

            // Set the user's password
            userEntity.Password = passwordHasher.HashPassword(userEntity, registrationDto.Password ?? string.Empty);

            // Set the user's role
            userEntity.Role = registrationDto.Role;

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
    /// <summary>
    /// Check if the user and password is correct
    /// </summary>
    /// <param name="signInDto">User dto with the user's data</param>
    /// <returns>Response with user data</returns>
    public async Task<ResponseDto<UserDto>> SignIn(SignInDto signInDto)
    {
        // Lets verify first if email and password exists
        if (signInDto.Email == null || signInDto.Password == null)
        {
            return new ResponseDto<UserDto>
            {
                Result = null,
                Message = Words.Get("IncorrectEmailPassword")
            };
        }

        try
        {
            // Create a filter definition to search for user by email
            FilterDefinition<UserEntity> filterDefinition = Builders<UserEntity>.Filter.Eq(m => m.Email, signInDto.Email);

            // Apply the filter in the search query
            UserEntity userEntity = await db.Users.Find(filterDefinition).FirstOrDefaultAsync();

            // Verify if the user exists
            if (userEntity != null)
            {
                // Verify if password is valid
                var result = passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password ?? string.Empty, signInDto.Password);

                // Verify if result is Success
                if (result != PasswordVerificationResult.Success)
                {
                    // Create the response
                    return new ResponseDto<UserDto>
                    {
                        Result = null,
                        Message = Words.Get("IncorrectEmailPassword")
                    };
                }

                // Create the response
                return new ResponseDto<UserDto>
                {
                    Result = new UserDto
                    {
                        UserId = userEntity.UserId,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Role = userEntity.Role,
                        Email = userEntity.Email
                    },
                    Message = Words.Get("SuccessSignIn")
                };
            }
            else
            {
                // Create the response
                return new ResponseDto<UserDto>
                {
                    Result = null,
                    Message = Words.Get("EmailNotFound")
                };
            }

        }
        catch (Exception ex)
        {

            // Logging the exception
            logger.LogError(ex, "An error occurred while sign in.");

            // Return error message
            return new ResponseDto<UserDto>
            {
                Result = null,
                Message = Words.Get("ErrorSignIn")
            };
        }
    }
}