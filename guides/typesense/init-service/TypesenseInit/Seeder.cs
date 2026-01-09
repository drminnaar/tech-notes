using Microsoft.Extensions.Logging;

namespace TypesenseInit;

public sealed class Seeder
{
    private readonly ILogger<Seeder> _logger;
    private readonly ProductCollection _productCollection;
    private readonly ProductSearchDocumentFileReader _productReader;

    public Seeder(ILogger<Seeder> logger, ProductCollection productCollection, ProductSearchDocumentFileReader productReader)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productCollection = productCollection ?? throw new ArgumentNullException(nameof(productCollection));
        _productReader = productReader ?? throw new ArgumentNullException(nameof(productReader));
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var productsJsonPath = Path.Combine("Data", "products.json");
        try
        {
            await _productCollection.DeleteCollectionIfExistsAsync(cancellationToken);
            await _productCollection.CreateCollectionAsync();
            var products = await _productReader.ReadProductsFromJsonAsync(productsJsonPath, cancellationToken);
            await _productCollection.ImportProductsAsync(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ An error occurred while seeding Typesense.");
        }
    }
}
