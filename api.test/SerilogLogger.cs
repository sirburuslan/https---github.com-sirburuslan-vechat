/*
 * @class SerilogLogger
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-20
 *
 * This class contains the SerilogLogger configuration which I'm using to detect issues in the test files
 */

// Main Namespace
namespace Api.Test;

public static class SerilogLogger
{
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs\\test-log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            /**
            SerilogLogger.Configure();
            Log.Information("This is a test log message.");
            **/
    }
}