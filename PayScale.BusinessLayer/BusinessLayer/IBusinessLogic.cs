using PayScale.Models.ViewModels;

namespace PayScale.BusinessLayer.BusinessLayer
{
    public interface IBusinessLogic
    {
        public TaxCalculationViewModel TaxCalculationLogic(int id);

    }
}
