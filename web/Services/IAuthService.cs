using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Web.Entities;

namespace Web.Services
{
    public interface IAuthService
    {
        public Task<User?> Register(UserDTO request);
        public Task<String?> Login(UserDTO request);
        
    }
}
