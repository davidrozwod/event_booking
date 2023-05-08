namespace event_booking.Services
{
    using Azure.Identity;
    using Azure.Security.KeyVault.Secrets;
    public class SendGridService
    {
        public static string GetSendGridApiKey()
        {
            var keyVaultUrl = "https://eventkeys.vault.azure.net";
            var secretName = "eventfulnz";
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(new Uri(keyVaultUrl), credential);

            KeyVaultSecret secret = client.GetSecret(secretName);

            return secret.Value;
        }
    }
}
