using CSVTest.Business.Services;
using CSVTest.Business.Services.Intergaces;
using CSVTest.DataAccess.Repositories;
using CSVTest.DataAccess.Repositories.Interfaces;

namespace CSVTest.ServiceConfiguration;

public static class  ServiceInjectorExtension
{
    public static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITripRepository, TripRepository>();
    }
    
    public static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped<ITripService, TripService>();
    }
}