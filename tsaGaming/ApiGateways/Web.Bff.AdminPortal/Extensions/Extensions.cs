using Web.Bff.AdminPortal.Services;
using Web.Bff.AdminPortal.Services.Interfaces;
using Services.Common.ReversedProxy;
using Services.Common.ReversedProxy.Extensions;
internal static class Extensions
{
    public static IServiceCollection AddReverseProxy(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy().LoadFromConfig(configuration.GetRequiredSection("ReverseProxy")).AddSwagger(configuration.GetRequiredSection("ReverseProxy"));

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddUrlGroup(_ => new Uri(configuration.GetRequiredValue("ProductUrlHC")), name: "productapi-check", tags: new string[] { "productapi" });
            

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //// Register delegating handlers
        //services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        // Register http services
        services.AddHttpClient<IProductApiClient, ProductApiClient>();
        //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        //services.AddTransient<GrpcExceptionInterceptor>();

        //services.AddScoped<IBasketService, BasketService>();

        //services.AddGrpcClient<Basket.BasketClient>((services, options) =>
        //{
        //    var basketApi = services.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcBasket;
        //    options.Address = new Uri(basketApi);
        //}).AddInterceptor<GrpcExceptionInterceptor>();

        //services.AddScoped<ICatalogService, CatalogService>();

        //services.AddGrpcClient<Catalog.CatalogClient>((services, options) =>
        //{
        //    var catalogApi = services.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcCatalog;
        //    options.Address = new Uri(catalogApi);
        //}).AddInterceptor<GrpcExceptionInterceptor>();

        //services.AddScoped<IOrderingService, OrderingService>();

        //services.AddGrpcClient<GrpcOrdering.OrderingGrpc.OrderingGrpcClient>((services, options) =>
        //{
        //    var orderingApi = services.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcOrdering;
        //    options.Address = new Uri(orderingApi);
        //}).AddInterceptor<GrpcExceptionInterceptor>();

        return services;
    }
}
