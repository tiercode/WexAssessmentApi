namespace WexAssessmentApi.Services
{
    public class IdentityServerSettings
    {
        public string? DiscoveryUrl { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string? ClientPassword { get; set; }
        public bool? UseHttps { get; set; }
    }
}
