using Freight.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Freight.Application;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFreightService, FreightService>();
    }
}
