using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Provider.Polly;
using Web.Bff.AdminPortal.Ocelot.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddOcelotConfig();
builder.AddServiceDefaults();
builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddGrpcServices();

builder.Services.AddCors(options =>
{
    // TODO: Read allowed origins from configuration
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    //app.UseSwaggerForOcelotUI();
//}

app.UseServiceDefaults();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;

}).UseOcelot().Wait();

app.MapControllers();

app.Run();
