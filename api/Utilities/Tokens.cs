/*
 * @class Tokens
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-06
 *
 * This class is used read the data from the jwt token
 */

// System Namespaces
using System.IdentityModel.Tokens.Jwt;

// App Utils
namespace api.Utilities;

/// <summary>
/// This class provides some methods to works with the Jwt tokens
/// </summary>
public class Tokens
{
    /// <summary>
    /// Get token data by claim field
    /// </summary>
    /// <param name="accessToken">Access token</param>
    /// <param name="field">Token field</param>
    /// <returns>string with field's data</returns>
    public string GetTokenData(string accessToken, string field)
    {
        // Default response
        var response = string.Empty;

        // Get the token handler
        var handler = new JwtSecurityTokenHandler();

        // Verify if the token is readable
        if (handler.CanReadToken(accessToken))
        {
            var jsonToken = handler.ReadJwtToken(accessToken);
            return jsonToken.Claims.FirstOrDefault(c => c.Type == field)?.Value ?? string.Empty;
        }

        return string.Empty;
    }
}