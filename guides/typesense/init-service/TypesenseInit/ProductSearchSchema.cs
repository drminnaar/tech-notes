using Typesense;

namespace TypesenseInit;

public class ProductSearchSchema
{
    public static Schema GetSchema(string collectionName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName);
        return new Schema(
            name: collectionName,
            fields:
            [
                // searchable fields
                new("name", FieldType.String, optional: false, facet: false),
                new("slug", FieldType.String, optional: false, facet: false),
                new("description", FieldType.String, optional: false, facet: false),
                new("searchTerms", FieldType.StringArray, optional: false, facet: false),

                // Display fields
                new("imageUrl", FieldType.String, optional: true, facet: false),

                // Numeric fields for range filtering and sorting
                new("rating", FieldType.Float, optional: false, facet: false),
                new("popularityScore", FieldType.Int32, optional: false, facet: false),
                new("reviewCount", FieldType.Int32, optional: false, facet: false),
                new("basePrice", FieldType.Float, optional: false, facet: true, sort: true, index: null),
                new("currentPrice", FieldType.Float, optional: false, facet: true, sort: true, index: null),
                new("listedAt", FieldType.Int64, optional: false, facet: false),
                new("updatedAt", FieldType.Int64, optional: false, facet: false),

                // faceted fields for filtering
                new("stockQuantity", FieldType.Int32, optional: false, facet: true),
                new("stockLevel", FieldType.String, optional: false, facet: true),
                new("type", FieldType.String, optional: false, facet: true),
                new("brand", FieldType.String, optional: false, facet: true),
                new("category", FieldType.String, optional: false, facet: true),
                new("subcategory", FieldType.String, optional: false, facet: true),
                new("colors", FieldType.ObjectArray, optional: false, facet: true),
            ],
            defaultSortingField: "currentPrice"
        )
        {
            EnableNestedFields = true
        };
    }
}
