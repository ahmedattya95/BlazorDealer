using AKSoftware.WebApi.Client;
using BlazorDealer.Models.Shared;
using System.Threading.Tasks;

namespace BlazorDealer.Client
{
    public class AuthService
    {
        private readonly ServiceClient _client;
        private readonly string _baseUrl;

        public AuthService(string url)
        {
            _client = new ServiceClient();
            _baseUrl = url;
        }

        public async Task<UserManageResponse> RegisterAsync(RegisterViewModel model)
        {
            return await _client.PostAsync<UserManageResponse>($"{_baseUrl}/auth/register", model);
        }

        public async Task<UserManageResponse> LoginAsync(LoginViewModel model)
        {
            return await _client.PostAsync<UserManageResponse>($"{_baseUrl}/auth/login", model);
        }
    }
}
