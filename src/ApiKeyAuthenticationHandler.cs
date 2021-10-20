using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Auth.ApiKey
{
    public interface IApiKeyValidation
    {
        Task<AuthenticateResult> ValidateAsync(string key);
    }

    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IApiKeyValidation _keyValidation;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IApiKeyValidation keyValidation) : base(options, logger, encoder, clock)
        {

            _keyValidation = keyValidation;

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            var keyName = Options.KeyName;
            var location = Options.Location;


            var endpoint = Context.Features.Get<IEndpointFeature>()?.Endpoint;

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return Task.FromResult(AuthenticateResult.NoResult());

            if (location == ApiKeyLocation.Query)
            {
                if (Request.Query.TryGetValue(keyName, out var queryValue))
                    return _keyValidation.ValidateAsync(queryValue);
            }

            if (Request.Headers.TryGetValue(keyName, out var headerValue))
                return _keyValidation.ValidateAsync(headerValue);


            return Task.FromResult(AuthenticateResult.Fail("Missing authorization value"));
        }
    }
}
