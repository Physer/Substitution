using Microsoft.Extensions.DependencyInjection;
using Substitution.Business.Interfaces;

namespace Substitution.Business
{
    public static class BusinessDependencyRegistrator
    {
        public static void RegisterBusinessDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ITypedHttpClient, TypedHttpClient>();
            
            services.AddTransient<IGenericHttpClient, GenericHttpClient>();
            services.AddTransient<ITypedHttpClient, TypedHttpClient>();
            services.AddSingleton<ITokenManager, TokenManager>();
        }
    }
}
