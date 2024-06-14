/*
 * @class SettingsController
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-08
 *
 * This class is used to read the settings for website and user
 */

// System Utils
using System.Dynamic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// Installed Utils
using MongoDB.Bson;

// App Utils
using api.Models.Dtos;
using api.Models.Dtos.Settings;
using api.Services.Interfaces.Users;
using api.Services.Interfaces.Settings;
using api.Utilities;

// Namespace for Controllers
namespace api.Controllers;

/// <summary>
/// Initializes a new instance of the <see cref="SettingsController"/> class.
/// </summary>
/// <param name="settingsRepository">An instance for the settings repository</param>
/// <param name="usersRepository">An instance for the users repository</param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/settings")]
public class SettingsController(ISettingsRepository settingsRepository, IUsersRepository usersRepository) : ControllerBase
{
    /// <summary>
    /// Gets the settings for website and user
    /// </summary>
    /// <returns>List with settings</returns>
    [HttpGet]
    [EnableCors("MainPolicy")]
    public async Task<IActionResult> GetSettings()
    {
        // Create a success response
        dynamic response = new ExpandoObject();

        // TEST

        // Lets create a new dictionary list
        Dictionary<string, string> optionsList2 = [];

        // Set response status
        response.success = true;

        optionsList2.Add("test", "it");

        // Set options
        response.website = optionsList2;

        // TEST

        // Get the options saved in the database
        ResponseDto<List<OptionDto>> savedOptions = await settingsRepository.OptionsListAsync();

        // Verify if options exists
        if (savedOptions.Result != null)
        {
            // Allowed options
            List<string> allowedOptions = [
                "WebsiteName",
                "DashboardLogoSmall",
                "DashboardLogoLarge",
                "SignInPageLogo",
                "HomePageLogo",
                "AnalyticsCode",
                "RegistrationEnabled",
                "PrivacyPolicy",
                "Cookies",
                "TermsOfService",
                "DemoVideo",
                "Ip2LocationEnabled",
                "Ip2LocationKey",
                "GoogleMapsEnabled",
                "GoogleMapsKey",
                "GoogleAuthEnabled",
                "GoogleClientId",
                "GoogleClientSecret",
                "ReCAPTCHAEnabled",
                "ReCAPTCHAKey"
            ];

            // Lets create a new dictionary list
            Dictionary<string, string> optionsList = [];

            // Get options length
            int optionsLength = savedOptions.Result.Count;

            // List the saved options
            for (int o = 0; o < optionsLength; o++)
            {
                // Check if the option is allowed
                if (!allowedOptions.Contains(savedOptions.Result[o].OptionName))
                {
                    continue;
                }

                // Add option to the dictionary
                optionsList.Add(savedOptions.Result[o].OptionName, savedOptions.Result[o].OptionValue!);
            }

            // Set response status
            response.success = true;

            // Set options
            response.website = optionsList;

        }

        // Verify if access token exists to get the user's data
        if (Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            // Get the access token
            string? token = authHeader.FirstOrDefault()?.Split(" ").LastOrDefault();

            // Verify if access token exists
            if (token is not null)
            {
                // Get the user's ID
                string userId = new Tokens().GetTokenData(token ?? string.Empty, "UserId");

                // Verify if the user id exists
                if (userId != "")
                {
                    // User the user's data
                    ResponseDto<UserDto> user = await usersRepository.GetUserAsync(ObjectId.Parse(userId));

                    // Verify if user exists
                    if (user.Result != null)
                    {
                        // Set response status
                        response.success = true;

                        // Add user to response
                        response.user = (Dictionary<string, string>)new() {
                            // Add User id
                            { "userId", user.Result.UserId.ToString() },

                            // Add First Name
                            { "firstName", user.Result.FirstName ?? string.Empty },

                            // Add Last Name
                            { "lastName", user.Result.LastName ?? string.Empty },

                            // Add Email
                            { "email", user.Result!.Email ?? string.Empty },

                            // Add Role
                            { "role", user.Result!.Role.ToString() ?? string.Empty }

                        };

                        // Get the user's settings
                        ResponseDto<List<UserOptionDto>> userOptionsList = await usersRepository.OptionsListAsync(ObjectId.Parse(userId));

                        // Verify if the options list exists
                        if (userOptionsList.Result != null)
                        {

                            // Get options length
                            int userOptionsLength = userOptionsList.Result.Count;

                            // List the options
                            for (int o = 0; o < userOptionsLength; o++)
                            {

                                // Add option
                                response.user.Add(char.ToLower(userOptionsList.Result[o].OptionName[0]) + userOptionsList.Result[o].OptionName[1..], userOptionsList.Result[o].OptionValue);

                            }

                        }

                    }

                }

            }

        }

        // Check if no data is to return
        if (((IDictionary<string, object>)response).Count > 0)
        {
            // Return the response
            return new JsonResult(response);
        }
        else
        {
            // Return a json
            return new JsonResult(new
            {
                success = false,
                message = savedOptions.Message
            });
        }
    }
}