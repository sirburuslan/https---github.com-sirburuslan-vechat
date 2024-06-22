/*
 * @class Registration Controller
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-18
 *
 * This class is used for users registration
 */

// System Namespaces
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// App Namespaces
using api.Models.Dtos;
using api.Models.Dtos.Auth.Users;
using api.Services.Interfaces.Users;

// Namespace for Auth Controllers
namespace api.Controllers.Auth;

/// <summary>
/// This controller is used to create new accounts
/// </summary>
/// <param name="usersRepository">An instance to the user repository</param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/[controller]")]
public class RegistrationController(IUsersRepository usersRepository) : ControllerBase
{
    /// <summary>
    /// This method validates the user's data and creates an account
    /// </summary>
    /// <returns>Success or error message</returns>
    [HttpPost]
    [EnableCors("MainPolicy")]
    public async Task<IActionResult> SignUp([FromBody] RegistrationDto registrationDto)
    {

        // Create user
        ResponseDto<UserDto> createUser = await usersRepository.RegisterUserAsync(registrationDto);

        // Verify if the account was created
        if (createUser.Result != null)
        {

            // Create a success response
            var response = new
            {
                success = true,
                message = createUser.Message
            };

            // Return a json
            return new JsonResult(response);
        }
        else
        {
            // Create a error response
            var response = new
            {
                success = false,
                message = createUser.Message
            };

            // Return a json
            return new JsonResult(response);
        }
    }
}