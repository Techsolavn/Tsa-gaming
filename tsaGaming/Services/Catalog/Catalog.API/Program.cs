using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Catalog.API;
using Catalog.API.Application.Behaviors;
using Catalog.API.Application.Commands;
using Catalog.API.Application.Queries;
using Catalog.API.Application.Validations;
using Catalog.API.Services;
using Catalog.Domain.Interfaces;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddHealthChecks(builder.Configuration);
builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddApplicationOptions(builder.Configuration);

var services = builder.Services;

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});

// Register the command validators for the validator behavior (validators based on FluentValidation library)
services.AddSingleton<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ICatalogRepository, CatalogRepository>();
var app = builder.Build();

app.UseServiceDefaults();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    var logger = app.Services.GetService<ILogger<CatalogContextSeed>>();

    await context.Database.MigrateAsync();
    await new CatalogContextSeed().SeedAsync(context, logger);
}

await app.RunAsync();
