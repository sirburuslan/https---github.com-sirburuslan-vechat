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
using static System.Text.Json.JsonSerializer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
// Installed Packages Namespaces
using Moq;
using MongoDB.Bson;
using Serilog;
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

        _mockOptions = new Mock<IOptions<AppSettings>>();
        _mockUsersRepository = new Mock<IUsersRepository>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs\\test-log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

    }
    
    private ResponseDto<UserDto> GetMockUserResponse(bool isSuccessful)
    {

        string objectIdString = "5f9a1b9b0c1d2e3f4b5c6d7e";
        MongoDB.Bson.ObjectId objectId = new MongoDB.Bson.ObjectId(objectIdString);

        return new ResponseDto<UserDto>
        {
            Result = isSuccessful ? new UserDto
            {
                UserId = objectId,
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

        // Arrange
        SetupMockOptions();
        var mockUserResponse = GetMockUserResponse(true);
        _mockUsersRepository.Setup(r => r.SignIn(It.IsAny<SignInDto>())).ReturnsAsync(mockUserResponse);

        var controller = new SignInController(_mockOptions.Object, _mockUsersRepository.Object);
        var signInDto = new SignInDto { Email = "john@example.com", Password = "password" };

        // Act
        IActionResult actionResult = await controller.SignIn(signInDto);

        // Assert
        var jsonResult = actionResult as JsonResult;
        Assert.NotNull(jsonResult);

        var resultValue = jsonResult.Value;
        Assert.NotNull(resultValue);
        //Log.Information();

        // Optionally, cast the result to a specific type if known
        var response = resultValue as dynamic;

        // Perform assertions
        Assert.True((bool)resultValue.GetType().GetProperty("success").GetValue(resultValue, null));
        Assert.Equal("Success", resultValue.GetType().GetProperty("message").GetValue(resultValue, null).ToString());

    }

}