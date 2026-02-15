using Microsoft.Extensions.Logging;
using Typesense;

namespace TypesenseInit;

public sealed class ProductCollection
{
    public const string CollectionName = "products";

    private readonly ILogger<ProductCollection> _logger;
    private readonly ITypesenseClient _typesenseClient;

    public ProductCollection(ILogger<ProductCollection> logger, ITypesenseClient typesenseClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _typesenseClient = typesenseClient ?? throw new ArgumentNullException(nameof(typesenseClient));
    }

    public async Task CreateCollectionAsync()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CollectionName);

        var schema = ProductSearchSchema.GetSchema(CollectionName);
        await _typesenseClient.CreateCollection(schema);

        _logger.LogInformation("✅ New collection '{CollectionName}' created", CollectionName);
    }

    public async Task DeleteCollectionIfExistsAsync(CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CollectionName);
        try
        {
            await _typesenseClient.RetrieveCollection(CollectionName, cancellationToken);
            _logger.LogInformation("📦 Collection '{CollectionName}' exists, deleting...", CollectionName);

            await _typesenseClient.DeleteCollection(CollectionName);
            _logger.LogInformation("✅ Old collection '{CollectionName}' deleted", CollectionName);
        }
        catch
        {
            _logger.LogInformation("📦 Collection '{CollectionName}' does not exist, creating new one...", CollectionName);
        }
    }

    public async Task ImportProductsAsync(ProductSearchDocument[] products, int batchSize = 40)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CollectionName);
        ArgumentNullException.ThrowIfNull(products);

        var importResults = await _typesenseClient.ImportDocuments(CollectionName, products, batchSize, ImportType.Upsert);

        if (importResults.Any(result => result.Success == false))
        {
            var failedImports = importResults.Where(result => result.Success == false).ToArray();
            _logger.LogError("❌ Failed to import {FailedCount} products into Typesense collection '{CollectionName}'", failedImports.Length, CollectionName);
            foreach (var failed in failedImports)
            {
                _logger.LogError("❌ Product import failed: {ErrorMessage}", failed.Error);
            }
            return;
        }

        var successes = importResults.Count(result => result.Success);

        _logger.LogInformation("✅  Seeded Typesense: {Successes}/{ProductCount} products imported.", successes, products.Length);
    }
}
