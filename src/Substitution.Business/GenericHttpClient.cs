using Newtonsoft.Json;
using Substitution.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Substitution.Business
{
    public class GenericHttpClient : IGenericHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GenericHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "users");
            var response = await client.SendAsync(requestMessage);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);
            return users;
        }
    }
}
