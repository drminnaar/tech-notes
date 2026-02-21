using System.Text.Json.Serialization;
using Example2.Shared.Models;

namespace Example2.Api.Models;

// public sealed record StartActionRequest(
//     Guid CustomerId,
//     CustomerActionType ActionType,
//     string Reason,
//     string RequestedBy
// );

public sealed record StartActionRequest
{
    public Guid CustomerId { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CustomerActionType ActionType { get; init; }

    public string Reason { get; init; } = string.Empty;

    public string RequestedBy { get; init; } = string.Empty;
}
