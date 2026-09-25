using Core.Application.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Singleton: shared clock for the process lifetime.
        services.AddSingleton<IClock, SystemClock>();

        // Transient: stateless demo service — new instance per resolve.
        services.AddTransient<IDemoErrorService, DemoErrorService>();

        // Scoped: per HTTP request (will map to DbContext scope when EF arrives).
        services.AddScoped<IDrawRepository, InMemoryDrawRepository>();

        return services;
    }
}
