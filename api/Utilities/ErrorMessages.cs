/*
 * @class Error Messages
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used to handle the errors messages in dtos
 */

// Namespace for General utilities
namespace api.Utilities;

/// <summary>
/// Errors Messages Manager
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Error message for missing email
    /// </summary>
    public static string EmailRequired => Words.Get("EmailRequired");

    /// <summary>
    /// Error message for too long email
    /// </summary>
    public static string EmailLong => Words.Get("EmailLong");

    /// <summary>
    /// Error message for missing password
    /// </summary>
    public static string PasswordRequired => Words.Get("PasswordRequired");

    /// <summary>
    /// Error message for too long password
    /// </summary>
    public static string PasswordLong => Words.Get("PasswordLong");

    /// <summary>
    /// Error message if first name has wrong length
    /// </summary>
    public static string FirstNameWrongLength => Words.Get("FirstNameWrongLength"); 

    /// <summary>
    /// Error message if last name has wrong length
    /// </summary>
    public static string LastNameWrongLength => Words.Get("LastNameWrongLength"); 

}