using Typesense.Setup;

namespace TypesenseInit.Configuration;

public sealed class TypesenseOptions
{
    public const string ConfigurationSectionName = "Typesense";
    public string ApiKey { get; set; } = string.Empty;
    public List<Node> Nodes { get; set; } = [];
}