
using PayScale.Models;
using PayScale.WebRazor.Clients.IClientServices;

namespace PayScale.WebRazor.Clients
{
    public class ClientService : IClientService
    {
        public PostalCodeTaxType CalculateTax(decimal amount, string postalCode)
        {
            throw new NotImplementedException();
        }

        public List<PostalCodeTaxType> GetPostalCodes()
        {
            throw new NotImplementedException();
        }

        public PostalCodeTaxType GetPostalCodesByCode(string postalCode)
        {
            throw new NotImplementedException();
        }
    }
}
