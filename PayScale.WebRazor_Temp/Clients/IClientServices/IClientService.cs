using PayScale.Models;

namespace PayScale.WebRazor.Clients.IClientServices
{
    public interface IClientService
    {
        public List<PostalCodeTaxType> GetPostalCodes();

        public PostalCodeTaxType GetPostalCodesByCode(string postalCode);

        public PostalCodeTaxType CalculateTax(decimal amount, string postalCode);


    }
}
