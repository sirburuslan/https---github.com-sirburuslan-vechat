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
// Installed Packages Namespaces
using Moq;
// App Namespaces
using api.Controllers.Auth;
using api.Models.Dtos.Auth.Users;
using api.Options;
using api.Services.Interfaces.Users;
using Xunit;

// Namespace for Controllers
namespace Api.Test.Controllers;

/// <summary>
/// SignInControllerTests Class
/// </summary>
public class SignInControllerTests
{
    /// <summary>
    /// Test Sign In
    /// </summary>
    [Fact]
    public async Task SignIn()
    {
        // Test Credentials
        SignInDto signInDto = new()
        {
            Email = "test@example.com",
            Password = "password123"
        };

        // Create a mock for AppSettings
        Mock<IOptions<AppSettings>> appSettingsMock = new();

        // Create a mock for the interface IUserRepository
        Mock<IUsersRepository> usersRepositoryMock = new();

        // Init the controller with using the Mock Object
        SignInController controller = new(appSettingsMock.Object, usersRepositoryMock.Object);

        // Send Request
        JsonResult? result = await controller.SignIn(signInDto) as JsonResult;

        if (result is JsonResult jsonResult)
        {

            // Process the response
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.GetType());
            Assert.NotNull(result.Value.GetType().GetProperty("success"));
            Assert.NotNull(result.Value.GetType().GetProperty("success")!.GetValue(result.Value));
            Assert.True((bool)result.Value.GetType().GetProperty("success")!.GetValue(result.Value)!);
            Assert.Equal("User signin successfully", result.Value.GetType().GetProperty("message")!.GetValue(result.Value));

        }
        else
        {
            // It is not a JsonResult
            Assert.Fail("Test failed because is not correct result.");
        }

    }
}