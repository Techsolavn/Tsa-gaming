public class UrlsConfig
{

    public class ProductOperations
    {
        // grpc call under REST must go trough port 80
        public static string GetItemById(int id) => $"/api/v1/product/{id}";
        public static string UpdateProduct() => "/api/v1/product";
    }

    public required string Product { get; set; }
}

