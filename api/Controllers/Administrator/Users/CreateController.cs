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
using System.Reflection;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Administrator.Users;
using api.Models.Entities.Users;
using api.Services.Interfaces.Users;
using api.Utilities;
using api.Utilities.Attributes;

// Namespace for Users Administrator Controller
namespace api.Controllers.Administrator.Users;

/// <summary>
/// Initializes a new instance of the <see cref="UpdateController"/> class.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/admin/users")]
[AdministratorAccess]
public class UpdateController(IUsersRepository usersRepository) : ControllerBase
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