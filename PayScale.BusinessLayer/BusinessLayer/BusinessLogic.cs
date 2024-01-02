using PayScale.DataAccess.Repository.IRepository;
using PayScale.Models.ViewModels;

namespace PayScale.BusinessLayer.BusinessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessLogic(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;

        }
        public TaxCalculationViewModel TaxCalculationLogic(int id)
        {
            TaxCalculationViewModel? data;
            var taxCalc = _unitOfWork.TaxCalculation.Get(filter: w => w.Id.Equals(id));

            var taxType = _unitOfWork.PostalCodeTaxType.Get(filter: w => w.PostalCode!.Id.Equals(taxCalc.PostalCodeId), includeProperties: "PostalCode,TaxType");
            var rates = _unitOfWork.TaxRate.Get(filter: w => w.TaxTypeId.Equals(taxType.Id), includeProperties: "TaxType");
            data = new TaxCalculationViewModel
            {
                AmountAfterTax = taxCalc.AmountAfterTax,
                TaxPercentage = $"{rates.Rate * 100}%",
                TaxType = rates.TaxType.TaxCalculationType,
            };
            return data;
        }
    }
}
