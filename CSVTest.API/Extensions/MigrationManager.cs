using Microsoft.EntityFrameworkCore;

namespace CSVTest.Extensions;

public static class MigrationManager
{
    /// <summary>
    ///     Migrates database on a given Database context type.
    /// </summary>
    /// <param name="app"></param>
    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
    {
        using var scope = host.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<T>();
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            var writer = Console.Error;
            writer.WriteLine(ex);
        }

        return host;
    }
}
