/*
 * @class RegistrationControllerTests
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-21
 *
 * This class contains the methods used for email registration testing
 */

// System Namespaces
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
// Installed Packages Namespaces
using Moq;
using MongoDB.Bson;
using Xunit;
// App Namespaces
using api.Controllers.Auth;
using api.Models.Dtos;
using api.Models.Dtos.Auth.Users;
using api.Options;
using api.Services.Interfaces.Users;

// Namespace for Controllers
namespace Api.Test.Controllers.Auth;

/// <summary>
/// RegistrationControllerTests Class
/// </summary>
public class RegistrationControllerTests
{
    
    // Mock for users repository
    private readonly Mock<IUsersRepository> _mockUsersRepository = new Mock<IUsersRepository>();

    /// <summary>
    /// Mock for Response
    /// </summary>
    /// <param name="isSuccessful">A boolean mark for the response</param>
    private ResponseDto<UserDto> GetMockResponse(bool isSuccessful)
    {

        // This is the mock response for success and fail login
        return new ResponseDto<UserDto>
        {
            Result = isSuccessful ? new UserDto
            {
                UserId = new MongoDB.Bson.ObjectId("5f9a1b9b0c1d2e3f4b5c6d7e"),
            } : null,
            Message = isSuccessful ? "Success" : "Error"
        };

    }

    /// <summary>
    /// Test Success Registration
    /// </summary>
    [Fact]
    public async Task Registration_ReturnsSuccessResponse()
    {

        // Get successfully user registration
        var mockSuccessResponse = GetMockResponse(true);

        // Add registration RegisterUserAsync method, dto with user's data and response in the repository 
        _mockUsersRepository.Setup(r => r.RegisterUserAsync(It.IsAny<RegistrationDto>())).ReturnsAsync(mockSuccessResponse);

        // Add mock to the controller
        RegistrationController controller = new (_mockUsersRepository.Object);

        // Registration data
        RegistrationDto registrationDto = new RegistrationDto {
            Email = "john@example.com",
            Password = "password"
        };

        // Lets register
        IActionResult actionResult = await controller.SignUp(registrationDto);

        // Convert IActionResult in JsonResult to
        // Access and verify the JSON data returned by the controller
        // Ensure the action method returned the expected JSON response type
        var jsonResult = actionResult as JsonResult;

        // Verify that jsonResult is not null
        Assert.NotNull(jsonResult);

        // Get the value from the json value
        var resultValue = jsonResult.Value;

        // Check if value is not null
        Assert.NotNull(resultValue);

        // Perform assertions
        Assert.True((bool?)resultValue.GetType()?.GetProperty("success")?.GetValue(resultValue, null) ?? false);
        Assert.Equal("Success", (string?)resultValue.GetType()?.GetProperty("message")?.GetValue(resultValue, null) ?? "");

    }

    /// <summary>
    /// Test Failed Registration
    /// </summary>
    [Fact]
    public async Task Registration_ReturnsFailedResponse()
    {

        // Get failed user registration
        var mockFailedResponse = GetMockResponse(false);

        // Add registration RegisterUserAsync method, dto with user's data and response in the repository 
        _mockUsersRepository.Setup(r => r.RegisterUserAsync(It.IsAny<RegistrationDto>())).ReturnsAsync(mockFailedResponse);

        // Add mock to the controller
        RegistrationController controller = new (_mockUsersRepository.Object);

        // Registration data
        RegistrationDto registrationDto = new RegistrationDto {
            Email = "john@example.com",
            Password = "password"
        };

        // Lets register
        IActionResult actionResult = await controller.SignUp(registrationDto);

        // Convert IActionResult in JsonResult to
        // Access and verify the JSON data returned by the controller
        // Ensure the action method returned the expected JSON response type
        var jsonResult = actionResult as JsonResult;

        // Verify that jsonResult is not null
        Assert.NotNull(jsonResult);

        // Get the value from the json value
        var resultValue = jsonResult.Value;

        // Check if value is not null
        Assert.NotNull(resultValue);

        // Perform assertions
        Assert.False((bool?)resultValue.GetType()?.GetProperty("success")?.GetValue(resultValue, null) ?? false);
        Assert.Equal("Error", (string?)resultValue.GetType()?.GetProperty("message")?.GetValue(resultValue, null) ?? "");

    }

}