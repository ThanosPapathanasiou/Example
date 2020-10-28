using Example.Api.Domain;
using Example.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Example.Api.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtSettings jwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "A user with this email address already exists" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            // TODO: if we want to switch to a user manager using a proper UserStore then switch the following lines
            // that way the password will be encrypted / salted in the database.
            // var createdUser = await userManager.CreateAsync(newUser, password);
            var createdUser = await userManager.CreateAsync(newUser);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("id", newUser.Id),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }), 
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult()
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
