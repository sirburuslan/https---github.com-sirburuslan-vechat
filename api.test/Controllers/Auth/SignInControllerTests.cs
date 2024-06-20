/*
 * @class SignInControllerTests
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-15
 *
 * This class contains the methods used for email sign in testing
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
/// SignInControllerTests Class
/// </summary>
public class SignInControllerTests
{
    private readonly Mock<IOptions<AppSettings>> _mockOptions;
    private readonly Mock<IUsersRepository> _mockUsersRepository;

    public SignInControllerTests()
    {

        // Create a mock for the App Settings
        _mockOptions = new Mock<IOptions<AppSettings>>();

        // Create a mock for the Users Repository
        _mockUsersRepository = new Mock<IUsersRepository>();

    }
    
    private ResponseDto<UserDto> GetMockUserResponse(bool isSuccessful)
    {

        // This is the mock response for success and fail login
        return new ResponseDto<UserDto>
        {
            Result = isSuccessful ? new UserDto
            {
                UserId = new MongoDB.Bson.ObjectId("5f9a1b9b0c1d2e3f4b5c6d7e"),
                FirstName = "John",
                LastName = "Doe",
                Role = 0,
                Email = "john@example.com"
            } : null,
            Message = isSuccessful ? "Success" : "Error"
        };

    }

    private void SetupMockOptions()
    {

        // Add fake website settings to the mock options 
        _mockOptions.Setup(o => o.Value).Returns(new AppSettings
        {
            JwtSettings = new AppSettings.JwtSettingsFormat
            {
                Key = "ThisIsALongerSecretKeyForTesting",
                Issuer = "YourIssuer",
                Audience = "YourAudience"
            },
            SiteUrl = "https://example.com",
            Storage = new AppSettings.StorageFormat
            {
                Default = "default",
                List = new Dictionary<string, AppSettings.StorageOptions>
                {
                    { "default", new AppSettings.StorageOptions { ClientId = "clientId" } }
                }
            },
            Logging = new AppSettings.LoggingOptions
            {
                LogLevel = new Dictionary<string, string>
                {
                    { "Default", "Information" }
                }
            },
            AllowedHosts = "*",
            ConnectionStrings = new AppSettings.ConnectionStringsFormat
            {
                DefaultConnection = "DefaultConnectionString"
            }
        });

    }

    [Fact]
    public async Task SignIn_ValidCredentials_ReturnsSuccessResponse()
    {

        // Load fake options
        SetupMockOptions();

        // Get the successfully user response
        var mockUserResponse = GetMockUserResponse(true);

        // Add login dto and respponse
        _mockUsersRepository.Setup(r => r.SignIn(It.IsAny<SignInDto>())).ReturnsAsync(mockUserResponse);

        // Add mock to the controller
        SignInController controller = new(_mockOptions.Object, _mockUsersRepository.Object);

        // Login data
        SignInDto signInDto = new SignInDto {
            Email = "john@example.com",
            Password = "password"
        };

        // Lets sign in 
        IActionResult actionResult = await controller.SignIn(signInDto);

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

    [Fact]
    public async Task SignIn_InvalidCredentials_ReturnsErrorResponse()
    {

        // Load the fake options
        SetupMockOptions();

        // Get failed login response
        var mockUserResponse = GetMockUserResponse(false);

        // Add login dto and respponse
        _mockUsersRepository.Setup(r => r.SignIn(It.IsAny<SignInDto>())).ReturnsAsync(mockUserResponse);

        // Add mock to the controller
        SignInController controller = new(_mockOptions.Object, _mockUsersRepository.Object);

        // Login details
        var signInDto = new SignInDto {
            Email = "john@example.com",
            Password = "password"
        };

        // Sign in 
        IActionResult actionResult = await controller.SignIn(signInDto);

        // Convert IActionResult in JsonResult to
        // Access and verify the JSON data returned by the controller
        // Ensure the action method returned the expected JSON response type
        var jsonResult = actionResult as JsonResult;

        // Check if jsonResult is not null
        Assert.NotNull(jsonResult);

        // Get the value from the json value
        var resultValue = jsonResult.Value;

        // Check if value is not null
        Assert.NotNull(resultValue);

        // Perform assertions
        Assert.False((bool?)resultValue.GetType()?.GetProperty("success")?.GetValue(resultValue, null) ?? false);

    }

}