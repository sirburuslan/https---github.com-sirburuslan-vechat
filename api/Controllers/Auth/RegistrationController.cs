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
using Microsoft.Extensions.Options;

// App Namespaces
using api.Models.Dtos;
using api.Models.Dtos.Auth.Users;
using api.Options;
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
public class RegistrationController(IUsersRepository usersRepository) : Controller
{
    /// <summary>
    /// This method validates the user's data and creates an account
    /// </summary>
    /// <returns>Success or error message</returns>
    [HttpPost]
    [EnableCors("MainPolicy")]
    public async Task<IActionResult> SignUp([FromBody] RegistrationDto registrationDto)
    {
        // Get the options saved in the database
        /*ResponseDto<List<Models.Dtos.Settings.OptionDto>> savedOptions = await settingsRepository.OptionsListAsync();

        // Lets create a new dictionary list
        Dictionary<string, string> optionsList = new();

        // Verify if options exists
        if ( savedOptions.Result != null ) {

            // Get options length
            int optionsLength = savedOptions.Result.Count;

            // List the saved options
            for ( int o = 0; o < optionsLength; o++ ) {

                // Add option to the dictionary
                optionsList.Add(savedOptions.Result[o].OptionName, savedOptions.Result[o].OptionValue!);

            }

        }

        // Get registration status
        optionsList.TryGetValue("RegistrationEnabled", out string? RegistrationEnabled);

        // Verify if the registration is enabled
        if ( RegistrationEnabled != "1" ) {

            // Return error response
            return new JsonResult(new {
                success = false,
                message = new Strings().Get("RegistrationDisabled")
            });

        }*/

        // Set user role
        registrationDto.Role = 1;

        // Create user
        ResponseDto<UserDto> createUser = await usersRepository.RegisterUserAsync(registrationDto);

        // Verify if the account was created
        if (createUser.Result != null)
        {
            // Save event
            /*await eventsRepository.CreateEventAsync(createUser.Result.UserId, 1);

            // Create email body content
            string body = "<p>" + new Strings().Get("LoginCredentials") + ":</p><div class=\"credentials\"><p><span class=\"email\">" + new Strings().Get("Email") + ":</span> <span>" + newUserDto.Email + "</span></p><p><span>" + new Strings().Get("Password") + ":</span> <span>" + newUserDto.Password + "</span></p></div><p>" + new Strings().Get("BestRegards") + "</p>";

            // Send email
            await new Sender().Send(optionsList, newUserDto.Email ?? string.Empty, new Strings().Get("WelcomeToSite"), body);*/

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