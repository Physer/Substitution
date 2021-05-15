using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Substitution.Business;
using System;
using System.Security.Claims;
using Xunit;

namespace Substitution.Tests
{
    public class TokenManagerTests
    {
        [Fact]
        public async void TestAccessTokenIsReturnedFromAccessor()
        {
            // Arrange
            var expectedAccessTokenValue = "valid_access_token";
            var authenticationService = Substitute.For<IAuthenticationService>();
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.GetService(typeof(IAuthenticationService)).Returns(authenticationService);

            var authenticationResult = AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), string.Empty));
            authenticationResult.Properties.StoreTokens(new[]
            {
                new AuthenticationToken
                {
                    Name = "access_token",
                    Value = expectedAccessTokenValue
                }
            });
            authenticationService.AuthenticateAsync(null, null).ReturnsForAnyArgs(authenticationResult);

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            httpContextAccessor.HttpContext.RequestServices = serviceProvider;

            var tokenManager = new TokenManager(httpContextAccessor);

            // Act
            var result = await tokenManager.GetAccessToken();

            // Assert
            Assert.Equal(expectedAccessTokenValue, result);
        }
    }
}
