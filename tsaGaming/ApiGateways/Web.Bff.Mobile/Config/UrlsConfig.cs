public class UrlsConfig
{

    public class CatalogOperations
    {
        // grpc call under REST must go trough port 80
        public static string GetAll() => $"/api/v1/catalog/";
    }

    public required string Catalog { get; set; }
}

