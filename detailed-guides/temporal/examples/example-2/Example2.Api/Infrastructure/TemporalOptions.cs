namespace Example2.Api.Infrastructure;

public sealed class TemporalOptions
{
    public const string SectionName = "Temporal";

    public string Host { get; init; } = "localhost:7233";
    public string Namespace { get; init; } = "default";
}
