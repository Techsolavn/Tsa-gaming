using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Configuration;
using Web.Bff.AdminPortal.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.AddServiceDefaults();
builder.Services.AddReverseProxy(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddHealthChecks(builder.Configuration);

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

builder.Services.Configure<UrlsConfig>(builder.Configuration.GetSection("urls"));



var app = builder.Build();

////Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    //app.UseSwaggerForOcelotUI();
//}

app.UseServiceDefaults();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapReverseProxy();



app.MapControllers();



app.Run();
