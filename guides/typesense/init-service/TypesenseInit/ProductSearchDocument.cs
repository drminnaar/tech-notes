using System.Text.Json.Serialization;

namespace TypesenseInit;

public sealed class ProductSearchDocument
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("sku")]
    public required string Sku { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("slug")]
    public required string Slug { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("imageUrl")]
    public required Uri ImageUrl { get; set; }

    [JsonPropertyName("colors")]
    public required Color[] Colors { get; set; }

    [JsonPropertyName("brand")]
    public required string Brand { get; set; }

    [JsonPropertyName("category")]
    public required string Category { get; set; }

    [JsonPropertyName("subcategory")]
    public required string Subcategory { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("stockQuantity")]
    public long StockQuantity { get; set; }

    [JsonPropertyName("stockLevel")]
    public required string StockLevel { get; set; }

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("reviewCount")]
    public long ReviewCount { get; set; }

    [JsonPropertyName("popularityScore")]
    public long PopularityScore { get; set; }

    [JsonPropertyName("basePrice")]
    public double BasePrice { get; set; }

    [JsonPropertyName("currentPrice")]
    public double CurrentPrice { get; set; }

    [JsonPropertyName("isOnSale")]
    public bool IsOnSale { get; set; }

    [JsonPropertyName("salePercentage")]
    public long SalePercentage { get; set; }

    [JsonPropertyName("searchTerms")]
    public required string[] SearchTerms { get; set; }

    [JsonPropertyName("listedAt")]
    public long ListedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public long UpdatedAt { get; set; }
}

public sealed class Color
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("hex")]
    public required string Hex { get; set; }
}
