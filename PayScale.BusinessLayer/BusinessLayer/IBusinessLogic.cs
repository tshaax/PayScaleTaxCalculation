using PayScale.Models.ViewModels;

namespace PayScale.BusinessLayer.BusinessLayer
{
    public interface IBusinessLogic
    {
        public TaxCalculationViewModel TaxCalculationLogic(int id);

        public TaxCalculationViewModel TaxCalculationLogic(decimal amount, string postalCode);

        public TaxCalculationViewModel TaxCalculationLogic(decimal amount, int postalCodeId);

    }
}
