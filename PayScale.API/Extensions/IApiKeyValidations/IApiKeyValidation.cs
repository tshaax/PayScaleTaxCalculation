namespace PayScale.API.Extensions.IApiKeyValidations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiKeyValidation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userApiKey"></param>
        /// <returns></returns>
        bool IsValidApiKey(string userApiKey);
    }
}
