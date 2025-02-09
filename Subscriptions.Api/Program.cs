using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Subscriptions.Data;

var builder = WebApplication.CreateBuilder(args);

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
        .WithOrigins("https://localhost:7210/"));
});

// Get the Azure Key Vault endpoint from the configuration
var keyVaultEndpointUri = builder.Configuration["AzureKeyVault:Vault"];
if (string.IsNullOrEmpty(keyVaultEndpointUri))
{
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

// NOTE: This will pull from the UserSecrets file...might want to use this as a backup
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

app.UseHttpsRedirection();
app.Run();
