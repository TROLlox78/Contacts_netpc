using Shared.DTOs;

namespace Blazor.Services
{
    public interface IAuthService
    {
        public Task<HttpResponseMessage?> GetToken(UserDTO userDTO);
        public void StoreToken(string token);
    }
}
