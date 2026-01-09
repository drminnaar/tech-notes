using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace TypesenseInit;

public sealed class ProductSearchDocumentFileReader
{
    private readonly ILogger<ProductSearchDocumentFileReader> _logger;

    public ProductSearchDocumentFileReader(ILogger<ProductSearchDocumentFileReader> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ProductSearchDocument[]> ReadProductsFromJsonAsync(string pathToProductsJson, CancellationToken cancellationToken)
    {
        if (!File.Exists(pathToProductsJson))
        {
            _logger.LogWarning("⚠️  No products.json found in data directory");
            _logger.LogInformation("📝 Please create Data/products.json with your product data");
            return [];
        }

        var jsonContent = await File.ReadAllTextAsync(pathToProductsJson, cancellationToken);
        var products = JsonSerializer.Deserialize<List<ProductSearchDocument>>(jsonContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (products == null || products.Count == 0)
        {
            _logger.LogWarning("⚠️  No products found in JSON file");
            return [];
        }

        _logger.LogInformation("📄 Found {ProductCount} products to import", products.Count);

        return [.. products];
    }
}
