using Microsoft.Extensions.Options;
using Temporalio.Client;

namespace Example2.Api.Infrastructure;

public sealed class TemporalClientService(IOptions<TemporalOptions> options)
: IHostedService
{
    private ITemporalClient? _client;

    public ITemporalClient Client => _client
        ?? throw new InvalidOperationException("Temporal client has not been initialised yet.");

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _client = await TemporalClient.ConnectAsync(new TemporalClientConnectOptions
        {
            TargetHost = options.Value.Host,
            Namespace = options.Value.Namespace
        });
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
