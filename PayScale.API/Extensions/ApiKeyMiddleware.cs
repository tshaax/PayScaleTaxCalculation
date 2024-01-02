using PayScale.API.Extensions.IApiKeyValidations;
using PayScale.Models.Constants;
using System.Net;

namespace PayScale.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidation _apiKeyValidation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="apiKeyValidation"></param>
        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Headers[APIKeyConstants.ApiKeyHeaderName]))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            string? userApiKey = context.Request.Headers[APIKeyConstants.ApiKeyHeaderName];

            if (!_apiKeyValidation.IsValidApiKey(userApiKey!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
