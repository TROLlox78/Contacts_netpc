using Shared.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;

        public AuthService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage?> GetToken(UserDTO userDTO)
        { // httpclient is connected over https
            return await httpClient.PostAsJsonAsync<UserDTO>("/api/auth/login", userDTO);
        }

        public void StoreToken(string token)
        {
            // I choose not to store it in localstorage
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
