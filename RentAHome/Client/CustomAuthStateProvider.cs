using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace RentAHome.Client
{
    public class CustomAuthStateProvider: AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;
        public CustomAuthStateProvider(ILocalStorageService _localStorageService, HttpClient http)
        {
            this._localStorageService = _localStorageService;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {

                    identity = new ClaimsIdentity(ParseClaimFromJwt(authToken), "jwt");
                    var expiry = identity.Claims.Where(w => w.Type.Equals("exp")).FirstOrDefault();
                    var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value)).LocalDateTime;
                    var diff = DateTime.Compare(datetime, DateTime.Now);
                    if (diff < 0)
                        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                    else
                        _http.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                }
                catch
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;


        }
        private byte[] ParseBase64WihoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        private IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBYTES = ParseBase64WihoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBYTES);
            var claims = keyValuePairs!.Select(w => new Claim(w.Key, w.Value.ToString()));
            return claims;
        }
    }
}
