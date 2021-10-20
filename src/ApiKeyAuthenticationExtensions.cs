using System;

using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Auth.ApiKey
{
    public static class ApiKeyAuthenticationExtensions
    {

        public static void ApiKeyAuthentication(this IServiceCollection services, Action<ApiKeyAuthenticationOptions> configureOptions)
        {

            services
                .AddAuthentication(ApiKeyAuthenticationDefaults.AuthenticationScheme)
                .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationDefaults.AuthenticationScheme, configureOptions);
        }
    }
}
