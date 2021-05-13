using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Substitution.Business.Interfaces;
using System.Text;

namespace Substitution.Business
{
    public static class BusinessDependencyRegistrator
    {
        public static void RegisterBusinessDependencies(this IServiceCollection services, TokenData tokenData)
        {
            var signingSecret = Encoding.UTF8.GetBytes(tokenData?.SigningSecret);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = "https://localhost:5001/",
                    ValidIssuer = "https://localhost:5001/",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingSecret),
                    ValidateLifetime = true,
                };
            });
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ITypedHttpClient, TypedHttpClient>();
            
            services.AddTransient<IGenericHttpClient, GenericHttpClient>();
            services.AddTransient<ITypedHttpClient, TypedHttpClient>();
            services.AddSingleton<ITokenManager, TokenManager>();
        }
    }
}
