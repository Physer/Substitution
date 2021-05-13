using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Substitution.Business;
using Substitution.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly TokenData _clientData;
        private readonly ITokenManager _tokenManager;

        public TokenController(IOptions<TokenData> clientOptions,
            ITokenManager tokenManager)
        {
            _clientData = clientOptions.Value;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenRequestModel tokenRequestModel)
        {
            if (string.IsNullOrWhiteSpace(tokenRequestModel?.Id) || string.IsNullOrWhiteSpace(tokenRequestModel?.Secret))
                return BadRequest();

            if (!tokenRequestModel.Id.Equals(_clientData.ClientId, StringComparison.InvariantCultureIgnoreCase) && !tokenRequestModel.Secret.Equals(_clientData.ClientSecret))
                return Unauthorized();

            var accessToken = _tokenManager.GenerateAccessToken(tokenRequestModel.Id, tokenRequestModel.Secret, _clientData.SigningSecret);
            if (string.IsNullOrWhiteSpace(accessToken))
                return BadRequest();

            return Ok(new
            {
                Token = accessToken,
                Type = "Bearer"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var accessToken = await _tokenManager.GetAccessToken();
            if (string.IsNullOrWhiteSpace(accessToken))
                return BadRequest();

            return Ok(accessToken);
        }
    }
}
