using Newtonsoft.Json;
using Substitution.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Substitution.Business
{
    public class TypedHttpClient : ITypedHttpClient
    {
        private readonly HttpClient _httpClient;

        public TypedHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "users");
            var response = await _httpClient.SendAsync(requestMessage);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);
            return users;
        }
    }
}
