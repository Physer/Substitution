using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Substitution.Business.Interfaces;
using System.Threading.Tasks;

namespace Substitution.Business
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetAccessToken() => await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
    }
}
