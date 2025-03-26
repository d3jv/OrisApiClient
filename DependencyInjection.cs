using Microsoft.Extensions.DependencyInjection;

namespace OrisApi;

public static class DependencyInjection
{
    public static void RegisterORISDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IOrisClient, OrisClient>();
    }
}
