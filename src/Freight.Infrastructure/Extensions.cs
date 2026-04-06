using Freight.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Freight.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IViaCepService, ViaCepService>();
    }
}
