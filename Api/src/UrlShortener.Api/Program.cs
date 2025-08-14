using Azure.Identity;
using UrlShortener.Api.Extensions;
using UrlShortener.Infrastructure.Extensions;
using UrlShortener.Core.Urls.Add;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVaultName"];
if(!string.IsNullOrEmpty(keyVaultName))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{keyVaultName}.vault.azure.net/"),
        new DefaultAzureCredential());
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddOpenApi();
builder.Services
    .AddUrlFeature()
    .AddCosmosDbUrlDataStore(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("api/urls", async (AddUrlHandler handler, AddUrlRequest request, CancellationToken ct) =>
{
    var requestWithUser = request with
    {
        CreatedBy = "Caio"
    };
    
    var result = await handler.HandleAsync(requestWithUser, ct);
    
    if (!result.Succeeded)
    {
        return Results.BadRequest(result.Error);
    }
    
    return Results.Created($"api/urls/{result.Value!.ShortUrl}", result.Value);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
