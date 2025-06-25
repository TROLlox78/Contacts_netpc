using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Data;
using Web.Entities;

namespace Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly ContactsDbContext contactsDb;
        private readonly IConfiguration configuration;

        public AuthService(ContactsDbContext contactsDb, IConfiguration configuration)
        {
            this.contactsDb = contactsDb;
            this.configuration = configuration;
        }

        public async Task<string?> Login(UserDTO request)
        {
            // 
            // check by username because it can't repeat
            var user = await contactsDb.Users.FirstOrDefaultAsync(x => request.Username == x.Username);
            if (user == null) { return null; }
            
            var check = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHashed, request.Password);
            if (check == PasswordVerificationResult.Failed) { return null; }
            if (check == PasswordVerificationResult.Success)
            {
                return CreateToken(user);
            }
            return CreateToken(user);
        }

        public async Task<User?> Register(UserDTO request)
        {
            var exists = await contactsDb.Users.AnyAsync(x=>request.Username==x.Username);
            if (exists) { return null; }
            User user = new();
            var passwordHashed = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.Username = request.Username;
            user.PasswordHashed = passwordHashed;

            contactsDb.Add(user);
            await contactsDb.SaveChangesAsync();

            return user;
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                //new Claim(ClaimTypes.id, user.Id)
            };
            // TODO: switch to an asymmetric key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("Auth:Token")!)
                );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                audience: configuration.GetValue<string>("Auth:Audience"),
                issuer: configuration.GetValue<string>("Auth:Issuer"),
                signingCredentials: credentials,
                expires: DateTime.Now.AddDays(1),
                claims: claims
                ); // TODO: understand this better
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
