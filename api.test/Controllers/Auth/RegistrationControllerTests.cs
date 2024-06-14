/*
 * @class RegistrationControllerTests
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-15
 *
 * This class contains the methods used for email registration testing
 */

// System Namespaces
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
// Installed Packages Namespaces
using Moq;
// App Namespaces
using api.Controllers.Auth;
using api.Models.Dtos;
using api.Models.Dtos.Auth.Users;
using api.Options;
using api.Services.Interfaces.Users;
using Xunit;


// Namespace for Controllers
namespace Api.Test.Controllers;

/// <summary>
/// RegistrationControllerTests Class
/// </summary>
public class RegistrationControllerTests
{

    /// <summary>
    /// Simulate a success signup
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task SignUpSuccess()
    {

        // Test sign up details
        RegistrationDto registrationDto = new()
        {
            Email = "test@example.com",
            Password = "password"
        };

        // Create a test response
        ResponseDto<UserDto> createUserResponse = new()
        {
            Result = new UserDto(),
            Message = "User created successfully"
        };

        // Create a mock for the interface IUserRepository
        Mock<IUsersRepository> usersRepositoryMock = new();

        // Create a simulation for the method RegisterUserAsync with predefined parameters
        usersRepositoryMock.Setup(repo => repo.RegisterUserAsync(registrationDto))
                                .ReturnsAsync(createUserResponse);

        // Lets instantiate the controller
        RegistrationController controller = new(usersRepositoryMock.Object);

        // Send Request
        JsonResult? result = await controller.SignUp(registrationDto) as JsonResult;

        // Check if result is JsonResult format
        if (result is JsonResult jsonResult)
        {

            // Lets test the response
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.GetType());
            Assert.NotNull(result.Value.GetType().GetProperty("success"));
            Assert.NotNull(result.Value.GetType().GetProperty("success")!.GetValue(result.Value));
            Assert.True((bool)result.Value.GetType().GetProperty("success")!.GetValue(result.Value)!);
            Assert.Equal("User created successfully", result.Value.GetType().GetProperty("message")!.GetValue(result.Value));

        }
        else
        {
            // It is not a JsonResult
            Assert.Fail("Test failed because is not correct result.");
        }

    }

    /// <summary>
    /// Simulate a failed signup
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task SignUpFailed()
    {

        // Test sign up details
        RegistrationDto registrationDto = new(); ;

        // Create a test response
        ResponseDto<UserDto> createUserResponse = new()
        {
            Result = null,
            Message = "Registration Failed"
        };

        // Create a mock for the interface IUserRepository
        Mock<IUsersRepository> usersRepositoryMock = new();

        // Create a simulation for the method RegisterUserAsync with predefined parameters
        usersRepositoryMock.Setup(repo => repo.RegisterUserAsync(registrationDto))
                                .ReturnsAsync(createUserResponse);

        // Lets instantiate the controller
        RegistrationController controller = new(usersRepositoryMock.Object);

        // Send Request
        JsonResult? result = await controller.SignUp(registrationDto) as JsonResult;

        // Check if result is JsonResult format
        if (result is JsonResult jsonResult)
        {

            // Lets test the response
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.GetType());
            Assert.NotNull(result.Value.GetType().GetProperty("success"));
            Assert.NotNull(result.Value.GetType().GetProperty("success")!.GetValue(result.Value));
            Assert.False((bool)result.Value.GetType().GetProperty("success")!.GetValue(result.Value)!);
            Assert.Equal("Registration Failed", result.Value.GetType().GetProperty("message")!.GetValue(result.Value));

        }
        else
        {
            // It is not a JsonResult
            Assert.Fail("Test failed because is not correct result.");
        }

    }

}