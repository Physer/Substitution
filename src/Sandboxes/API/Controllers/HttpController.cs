using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitution.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpController : ControllerBase
    {
        private readonly IGenericHttpClient _genericHttpClient;
        private readonly ITypedHttpClient _typedHttpClient;
        private readonly ITokenManager _tokenManager;

        public HttpController(IGenericHttpClient genericHttpClient, ITypedHttpClient typedHttpClient, ITokenManager tokenManager)
        {
            _genericHttpClient = genericHttpClient;
            _typedHttpClient = typedHttpClient;
            _tokenManager = tokenManager;
        }

        [HttpGet("users/generic")]
        public async Task<IActionResult> GetUsersUsingGenericHttpClient()
        {
            var users = await _genericHttpClient.GetUsers();
            return Ok(users);
        }

        [HttpGet("users/typed")]
        public async Task<IActionResult> GetUsersUsingTypedHttpClient()
        {
            var users = await _typedHttpClient.GetUsers();
            return Ok(users);
        }
    }
}
