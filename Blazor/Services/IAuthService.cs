using Shared.DTOs;

namespace Blazor.Services
{
    public interface IAuthService
    {
        public Task<HttpResponseMessage?> GetToken(UserDTO userDTO);
        Task<HttpResponseMessage?> Register(UserDTO userDTO);
        public void StoreToken(string token);
    }
}
