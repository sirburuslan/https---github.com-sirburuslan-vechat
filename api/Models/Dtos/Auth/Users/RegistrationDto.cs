/*
 * @class Registration Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-19
 *
 * This class is used for user registration
 */

// System Namespaces
using System.ComponentModel.DataAnnotations;

// App Namespaces
using api.Utilities;
using api.Utilities.Validations;

// Namespace for Users Auth Dto
namespace api.Models.Dtos.Auth.Users;

/// <summary>
/// Registration Dto Class
/// </summary>
public class RegistrationDto
{
    /// <summary>
    /// User Email Field
    /// </summary>
    [RequiredValidation(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(200, ErrorMessageResourceName = "EmailLong", ErrorMessageResourceType = typeof(ErrorMessages))]
    public string? Email { get; set; }

    /// <summary>
    /// User Password Field
    /// </summary>
    [RequiredValidation(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(20, ErrorMessageResourceName = "PasswordLong", ErrorMessageResourceType = typeof(ErrorMessages))]
    public string? Password { get; set; }

    /// <summary>
    /// User's role field
    /// </summary>
    [NumberValidation(Minimum = 0, Maximum = 1, ErrorMessage = "SupportedValueShouldBe")]
    public int Role { get; set; }
}