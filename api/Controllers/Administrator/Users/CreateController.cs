/*
 * @class CreateController
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-13
 *
 * This class is used to create users
 */

// System Utils
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Administrator.Users;
using api.Services.Interfaces.Users;
using api.Utilities.Attributes;

// Namespace for Users Administrator Controllers
namespace api.Controllers.Administrator.Users;

/// <summary>
/// Initializes a new instance of the <see cref="CreateController"/> class.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/admin/users")]
[AdministratorAccess]
public class CreateController(IUsersRepository usersRepository) : ControllerBase
{

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="newUserDto">Data transfer object with user information</param>
    /// <returns>Message if the user was created or error</returns>
    [HttpPost("create")]
    [EnableCors("MainPolicy")]
    public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUserDto)
    {

        // Set user role
        newUserDto.Role = 1;

        // Create user
        ResponseDto<UserDto> createUser = await usersRepository.CreateUserAsync(newUserDto);

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