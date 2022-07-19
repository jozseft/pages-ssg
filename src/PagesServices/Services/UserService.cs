using Microsoft.Extensions.Options;
using PagesCommon.DTOs;
using PagesConfig;
using PagesData.Entities;
using PagesData.Interfaces;
using PagesServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace PagesServices.Services
{
    public class UserService : IUserService
    {
        private readonly SecretKeys _secretKeys;
        private readonly UserManager<User> _userManager;

        public UserService(IOptions<SecretKeys> secretKeys, UserManager<User> userManager)
        {
            _secretKeys = secretKeys.Value;
            _userManager = userManager;
        }

        public async Task<UserDTO> Authenticate(UserLoginRequestDTO model)
        {
            User user = _userManager.Users.SingleOrDefault(u => u.UserName == model.Username);

            if (user == null) return null;

            var userSigninResult = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!userSigninResult) return null;

            var token = GenerateJwtToken(user);

            return MapUserToUserDTO(user, token);
        }

        private UserDTO MapUserToUserDTO(User user, string token)
        {
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKeys.Auth);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
