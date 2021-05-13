using Microsoft.AspNetCore.Mvc;
using Substitution.Business.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpController : ControllerBase
    {
        private readonly IGenericHttpClient _genericHttpClient;
        private readonly ITypedHttpClient _typedHttpClient;

        public HttpController(IGenericHttpClient genericHttpClient, 
            ITypedHttpClient typedHttpClient)
        {
            _genericHttpClient = genericHttpClient;
            _typedHttpClient = typedHttpClient;
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
