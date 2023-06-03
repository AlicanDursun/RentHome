using Microsoft.AspNetCore.Components.Authorization;

namespace RentAHome.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient http,AuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> RegisterOrLogin(User user)
        {
            var result = await _http.PostAsJsonAsync("api/auth/RegisterOrLogin", user);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}
