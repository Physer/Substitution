using Newtonsoft.Json;
using Substitution.Business;
using Substitution.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Substitution.Tests
{
    public class TypedHttpClientTests
    {
        [Fact]
        public async void TestValidUsersResponseFromExternalParty()
        {
            // Arrange
            var expectedContent = new List<User>
            {
                new User
                {
                    Address = new Address(),
                    Company = new Company(),
                    Email = "unit_tests@substitution.com",
                    Id = 1,
                    Name = "Unit tests 1"
                },
                                new User
                {
                    Address = new Address(),
                    Company = new Company(),
                    Email = "unit_tests@substitution.com",
                    Id = 1,
                    Name = "Unit tests 2"
                },
            };
            
            var mockedHttpMessageHandler = new MockedHttpMessageHandler(expectedContent, HttpStatusCode.OK);
            var httpClient = new HttpClient(mockedHttpMessageHandler);
            var typedHttpClient = new TypedHttpClient(httpClient);

            // Act
            var result = await typedHttpClient.GetUsers();

            // Assert
            var expectedResultJson = JsonConvert.SerializeObject(expectedContent);
            var resultJson = JsonConvert.SerializeObject(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(expectedResultJson, resultJson);
        }
    }
}
