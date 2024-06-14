/*
 * @class Sign In Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used for user sign in
 */

// System Namespaces
using System.ComponentModel.DataAnnotations;

// App Namespaces
using api.Utilities;
using api.Utilities.Validations;

// Namespace for Users Auth Dto
namespace api.Models.Dtos.Auth.Users;

/// <summary>
/// Sign In Dto Class
/// </summary>
public class SignInDto
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
}