using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PayScale.Models;
using PayScale.Models.Constants;
using PayScale.Website.Clients.IClientServices;
using PayScale.Website.Models;

namespace PayScale.Website.Clients
{
    public class ClientService : IClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientOptions _options;
        private readonly string _baseUri;
        public ClientService(IHttpClientFactory httpClientFactory, IOptions<ClientOptions> options)
        {
            _baseUri = options.Value.Url;
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }
        public async Task<PostalCodeTaxType> CalculateTax(decimal amount, string postalCode)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(APIKeyConstants.ApiKeyHeaderName, _options.ApiKey);

            var apiUrl = $"{_baseUri}{_options?.Endpoints?.GetTaxCalculator}?amount={amount}&postalcode={postalCode}";
            var response =  await client.GetAsync(apiUrl);

            var results = JsonConvert.
                DeserializeObject<PostalCodeTaxType>(await response.Content.ReadAsStringAsync());

            return results;
        }

        public async Task<List<PostalCodeTaxType>>GetPostalCodes()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(APIKeyConstants.ApiKeyHeaderName, _options.ApiKey);

            var apiUrl = $"{_baseUri}{_options?.Endpoints?.GetPostalCodes}";
            var response = await client.GetAsync(apiUrl);

            var results = JsonConvert.
                DeserializeObject<List<PostalCodeTaxType>>(await response.Content.ReadAsStringAsync());

            return results;
        }

        public async Task<PostalCodeTaxType> GetPostalCodesByCode(string postalCode)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(APIKeyConstants.ApiKeyHeaderName, _options.ApiKey);

            var apiUrl = $"{_baseUri}{_options?.Endpoints?.GetPostalCodes}";
            var response = await client.GetAsync(apiUrl);

            var results = JsonConvert.
                DeserializeObject<PostalCodeTaxType>(await response.Content.ReadAsStringAsync());

            return results;
        }

    
    }
}
