using IdentityModel.Client;

namespace WexAssessmentApi.Services
{
    public interface ITokenServiceAsync
    {
        Task<TokenResponse> GetTokenAsync(string scope);
    }
}
