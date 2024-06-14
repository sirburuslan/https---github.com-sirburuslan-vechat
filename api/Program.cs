/*
 * @class Program
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-15
 *
 * This class is the entry point in the app
 */

// App namespace
namespace VeChat;

/// <summary>
/// Program class for main entry
/// </summary>
public static class Program
{
    /// <summary>
    /// Main class used as entry point for the app
    /// </summary>
    /// <param name="args">Command-line arguments passed when the application is started</param>
    public static void Main(string[] args)
    {
        // Run the application
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Create the Host and set Startup
    /// </summary>
    /// <param name="args">Command-line arguments passed when the application is started</param>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
