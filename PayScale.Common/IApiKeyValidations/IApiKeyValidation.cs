

namespace PayScale.Common.IApiKeyValidations
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
