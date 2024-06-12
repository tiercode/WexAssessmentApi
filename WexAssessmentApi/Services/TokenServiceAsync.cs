using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace WexAssessmentApi.Services
{
    public class TokenServiceAsync : ITokenServiceAsync
    {
        private readonly ILogger<TokenServiceAsync> _logger;
        private readonly IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;

        public TokenServiceAsync(ILogger<TokenServiceAsync> logger, IOptions<IdentityServerSettings> identityServerSettings)
        {
            _logger = logger;
            _identityServerSettings = identityServerSettings;

            using var httpClient = new HttpClient();

            // Get identity server client information from the appsettings file
            _discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoveryUrl).Result;

            if (_discoveryDocument.IsError)
            {
                logger.LogError($"Unable to obtain discovery document at this time. Error is: {_discoveryDocument.Error}");
                throw new Exception("Unable to obtain discovery docment", _discoveryDocument.Exception);
            }
        }

        /// <summary>
        /// Method to reach out to identity server and get bearer token
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TokenResponse> GetTokenAsync(string scope)
        {
            using var httpClient = new HttpClient();

            // Connect to the identity server via parameters from the appsettings file
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = _identityServerSettings.Value.ClientName,
                ClientSecret = _identityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            // Log and throw any and all errors
            if (tokenResponse.IsError) 
            {
                _logger.LogError($"Unable to obtain token at this time. Error is: {tokenResponse.Error}");
                throw new Exception($"Unable to obtain token: {tokenResponse.Error}", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
