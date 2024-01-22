using MechanicalInventory.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MechanicalInventory.Handlers
{
    public class TokenHandler
    {
        private readonly string _username;
        private readonly string _role;
        private readonly string _tokenKey;
        public TokenHandler(UserCredential userCredential, string tokenKey)
        {
            _username = userCredential.Username;
            _role = userCredential.Role;
            _tokenKey = tokenKey;
        }
        public async Task<string> GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenKey = Encoding.UTF8.GetBytes(_tokenKey);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name,_username),
                    new Claim(ClaimTypes.Role,_role)
                }),
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityTokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            var JwtToken = tokenHandler.WriteToken(token);
            TokenResponse tokenResponse = new TokenResponse { Token = JwtToken };
            return tokenResponse.Token;
        }
    }
}
