using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CSVTest.DataAccess.CSV;

public static class DatabaseExtensions
{
    public static IServiceCollection SeedCsvData<TContext, TEntity>(
        this IServiceCollection services,
        string csvFilePath,
        Func<TContext, DbSet<TEntity>> dbSetSelector,
        ClassMap<TEntity> csvMap
    ) where TContext : DbContext
        where TEntity : class
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.EnsureCreated();

        var dbSet = dbSetSelector(context);

        if (!dbSet.Any())
        {
            var data = ReadCsvFile<TEntity>(csvFilePath, csvMap);
            dbSet.AddRange(data);
            context.SaveChanges();
        }

        return services;
    }

    private static List<TEntity> ReadCsvFile<TEntity>(string filePath, ClassMap<TEntity> csvMap) where TEntity : class
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"CSV file not found at {filePath}");

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        });

        csv.Context.RegisterClassMap(csvMap);

        return csv.GetRecords<TEntity>().ToList();
    }
}