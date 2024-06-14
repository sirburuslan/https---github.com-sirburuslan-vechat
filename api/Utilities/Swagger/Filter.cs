/*
 * @class Filter
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-16
 *
 * This class contains a filter for Swagger which has the scope to remove urls without a version
 */

// System Namespaces
// Swagger Namespaces
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// Namespace for Swagger Utils
namespace api.Utilities.Swagger;

/// <summary>
/// Implements the <see cref="IDocumentFilter"/> interface to remove unversioned URLs from a Swagger document.
/// </summary>
public class Filter : IDocumentFilter
{
    /// <summary>
    /// Applies filtering logic to remove unversioned URLs from the provided Swagger document.
    /// </summary>
    /// <param name="swaggerDoc">The OpenApiDocument object representing the Swagger document.</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext filterContext)
    {
        // Filter the paths to keep only those containing the "/v" segment
        OpenApiPaths filteredPaths = [];

        foreach (var pathItem in swaggerDoc.Paths)
        {
            if (pathItem.Key.Contains("/v"))
            {
                filteredPaths.Add(pathItem.Key, pathItem.Value);
            }
        }

        // Assign the filtered paths to the swagger document
        swaggerDoc.Paths = filteredPaths;
    }
}
