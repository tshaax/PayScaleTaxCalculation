
using PayScale.API.Extensions.IApiKeyValidations;
using PayScale.Models.Constants;

namespace PayScale.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userApiKey"></param>
        /// <returns></returns>
        public bool IsValidApiKey(string userApiKey)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;
            string? apiKey = _configuration.GetValue<string>(APIKeyConstants.ApiKeyName);
            if (apiKey == null || apiKey != userApiKey)
                return false;
            return true;
        }
    }
}
