using Microsoft.IdentityModel.Tokens;
using Substitution.Business.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Substitution.Business
{
    public class TokenManager : ITokenManager
    {
        public async Task<string> GetAccessToken() => throw new NotImplementedException();

        public async Task<string> GenerateAccessToken(string id, string secret, string signingSecret)
        {
            if (string.IsNullOrWhiteSpace(signingSecret))
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(signingSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sub", id) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
