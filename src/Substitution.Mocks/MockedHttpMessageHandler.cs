using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Substitution.Mocks
{
    public class MockedHttpMessageHandler : HttpMessageHandler
    {
        private readonly object _response;
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// The default version of the mock returns a 200 OK with a string value of 'Success'
        /// </summary>
        public MockedHttpMessageHandler()
        {
            _response = new
            {
                Data = new
                {
                    Message = "Success"
                }
            };
            _statusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// A custom HTTP response
        /// </summary>
        /// <param name="response">A response message as string</param>
        /// <param name="statusCode">The desired returned HTTP status code</param>
        public MockedHttpMessageHandler(object response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => new HttpResponseMessage
        {
            StatusCode = _statusCode,
            Content = new StringContent(JsonConvert.SerializeObject(_response))
        };
    }
}
