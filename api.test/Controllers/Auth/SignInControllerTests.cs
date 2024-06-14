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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
// Installed Packages Namespaces
using Moq;
// App Namespaces
using api.Controllers.Auth;
using api.Models.Dtos.Auth.Members;
using api.Options;
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

        // Init the controller with using the Mock Object
        SignInController controller = new(appSettingsMock.Object);

        // Send Request
        JsonResult? result = await controller.SignIn(signInDto) as JsonResult;

        // Process the response
        Assert.NotNull(result);
        Assert.False((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
        Assert.NotNull(result.Value.GetType().GetProperty("message").GetValue(result.Value));

    }
}