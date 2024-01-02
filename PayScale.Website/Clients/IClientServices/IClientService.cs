using PayScale.Models;
using PayScale.Models.ViewModels;

namespace PayScale.Website.Clients.IClientServices
{
    public interface IClientService
    {
        public Task<List<PostalCodeTaxType>> GetPostalCodes();

        public Task<PostalCodeTaxType> GetPostalCodesByCode(string postalCode);

        public Task<PostalCodeTaxType> CalculateTax(decimal amount, string postalCode);

        public Task<TaxCalculationViewModel> SubmitCalculatedTax(TaxCalculation taxCalculation);

    }
}
