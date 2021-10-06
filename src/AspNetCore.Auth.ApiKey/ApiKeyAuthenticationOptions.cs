
using Microsoft.AspNetCore.Authentication;

namespace AspNetCore.Auth.ApiKey
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// ApiKey Name 
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// Key Location
        /// </summary>
        public ApiKeyLocation Location { get; set; }
    }
}
