namespace Example2.Api.Infrastructure;

public static class TemporalServiceExtensions
{
    public static IServiceCollection AddTemporalClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<TemporalOptions>(
            configuration.GetSection(TemporalOptions.SectionName));

        services.AddSingleton<TemporalClientService>();

        services.AddHostedService(sp => sp.GetRequiredService<TemporalClientService>());

        services.AddSingleton(sp => sp.GetRequiredService<TemporalClientService>().Client);

        return services;
    }
}
