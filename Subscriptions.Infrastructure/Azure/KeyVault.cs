using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Subscriptions.Infrastructure.Azure
{
    internal sealed class KeyVault(string vaultUri)
    {
        private readonly SecretClient _secretClient = new(new Uri(vaultUri), new DefaultAzureCredential());

        public string GetSecret(string secretName)
        {
            return _secretClient.GetSecret(secretName).Value.Value;
        }
    }
}
