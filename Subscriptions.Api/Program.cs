using Serilog;
using Subscriptions.Api.EndPoints;
using Subscriptions.Api.Interfaces;
using Subscriptions.Api.Public.EndPoints;
using Subscriptions.Api.Services;
using Subscriptions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// Setup CORS policy to allow requests from the specified origin (clients)
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("DefaultPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(origin => true)
        .WithOrigins("https://localhost:7059/"));
});

builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

// Get the Azure Key Vault endpoint from the configuration
var keyVaultEndpointUri = builder.Configuration["AzureKeyVault:Vault"];
if (string.IsNullOrEmpty(keyVaultEndpointUri))
{
    //TODO: Log this error
    throw new ArgumentNullException(nameof(keyVaultEndpointUri), "Azure Key Vault endpoint is not configured.");
}
var keyVaultEndpoint = new Uri(keyVaultEndpointUri);
// Add Azure Key Vault service to the container.
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Get the secret name to lookup from the configuration
var secretName = builder.Configuration["AzureKeyVault:SecretName"];
if (string.IsNullOrEmpty(secretName))
{
    // TODO: Log this error
    throw new ArgumentNullException(nameof(secretName), "Secret name is not configured.");
}
// Get the SQL connection string from Azure Key Vault
var sqlConnectionString = builder.Configuration[secretName];
if (string.IsNullOrEmpty(sqlConnectionString))
{
    // TODO: Log this error
    throw new ArgumentNullException(nameof(sqlConnectionString), "SQL connection string is not configured.");
}
builder.Services.AddDbContext<SubscriptionsContext>(options =>
{
    options.UseSqlServer(sqlConnectionString,
    options => options.EnableRetryOnFailure());
});

// TODO: Create passwordless connection to Azure SQL

// NOTE: This will pull from the appsettings.json file...might want to use this as a backup
// in case call to KeyVault fails or for local development.
string localConnectionString = builder.Configuration["LocalServices:localConnectionString"]!;

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["StorageConnection:blobServiceUri"]!).WithName("StorageConnection");
    clientBuilder.AddQueueServiceClient(builder.Configuration["StorageConnection:queueServiceUri"]!).WithName("StorageConnection");
    clientBuilder.AddTableServiceClient(builder.Configuration["StorageConnection:tableServiceUri"]!).WithName("StorageConnection");
});

var app = builder.Build();
app.UseCors("DefaultPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Microsoft.Extensions.Logging.ILogger _logger = app.Services.GetRequiredService<ILogger<Program>>();
_logger.LogDebug("================================================================================");

app.UseHttpsRedirection();
SusbcriptionTypeEndPoints.MapEndPoints(app, _ => { });
SubscriptionEndPoints.MapEndPoints(app, _ => { });
ApiTestEndPoints.MapEndPoints(app, _ => { });
app.Run();

_logger.LogDebug("================================================================================");
_logger.LogInformation("Application started");
