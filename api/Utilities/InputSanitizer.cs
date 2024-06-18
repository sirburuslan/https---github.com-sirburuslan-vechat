/*
 * @class Input Sanitizer
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-16
 *
 * This class is used sanitize the data
 */

// System Namespaces
using System.Text.RegularExpressions;

// App Utils
namespace api.Utilities;

/// <summary>
/// This class provides some methods to sanitize input
/// </summary>
public static partial class InputSanitizer
{

    /// <summary>
    /// Remove special characters
    /// </summary>
    /// <param name="text">Text to be sanitized</param>
    /// <returns>Sanitized</returns>
    public static string RemoveSpecialCharacters(string text)
    {
        // Check if text exists
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Trim whitespace
        text = text.Trim();

        // Allow for matching letters and numbers from various languages
        text = CustomRegex().Replace(text, string.Empty);

        return text;
    }

    [GeneratedRegex(@"[^\p{L}\p{N}\s]")]
    private static partial Regex CustomRegex();

}