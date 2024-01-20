using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Polly;
using Web.Bff.AdminPortal.Ocelot.Services;
using Web.Bff.AdminPortal.Ocelot.Services.Interfaces;

internal static class Extensions
{
    public static WebApplicationBuilder AddOcelotConfig(this WebApplicationBuilder builder)
    {
        var routes = "Routes";

        builder.Configuration.AddOcelotWithSwaggerSupport(options =>
        {
            options.Folder = routes;
        });

        builder.Services.AddOcelot(builder.Configuration).AddPolly();

        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
            .AddOcelot(routes, builder.Environment)
            .AddEnvironmentVariables();
        
        return builder;
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
