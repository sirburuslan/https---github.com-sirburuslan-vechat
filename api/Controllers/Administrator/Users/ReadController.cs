/*
 * @class ReadController
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-16
 *
 * This class is used to read users
 */

// System Utils
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// App Utils
using api.Services.Interfaces.Users;
using api.Utilities.Attributes;
using api.Utilities;
using api.Models.Dtos;

// Namespace for Users Administrator Controllers
namespace api.Controllers.Administrator.Users;

/// <summary>
/// Initializes a new instance of the <see cref="ReadController"/> class.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/admin/users")]
[AdministratorAccess]
public class ReadController(IUsersRepository usersRepository): ControllerBase {

    /// <summary>
    /// Get the list with users
    /// </summary>
    /// <returns>List with users or error message</returns>
    [HttpGet("list")]
    [EnableCors("MainPolicy")]
    public async Task<IActionResult> List(int page, string? search) {

        // Sanitize the search
        search = (search != null)?InputSanitizer.RemoveSpecialCharacters(search):"";

        // Create the search object
        SearchDto searchDto = new() {
            Page = page,
            Search = search
        };

        // Get all users
        ResponseDto<ElementsDto<UserDto>> usersList = await usersRepository.GetUsersAsync(searchDto);

        // Verify if users exists
        if ( usersList.Result != null ) {

            // Return users response
            return new JsonResult(new {
                success = true,
                usersList.Result,
                time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });

        } else {

            // Return error response
            return new JsonResult(new {
                success = false,
                message = usersList.Message
            });

        }

    }

}