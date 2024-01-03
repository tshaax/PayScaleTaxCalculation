using PayScale.DataAccess.Repository.IRepository;
using PayScale.Models;
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

        public TaxCalculationViewModel TaxCalculationLogic(decimal amount, string postalCode)
        {
            decimal? amountTaxRate = 0.0M;

            var rates = _unitOfWork.TaxRate.GetAll(includeProperties: "TaxType");

            var taxType = _unitOfWork.PostalCodeTaxType
                .Get(filter: w => w.PostalCode!.Code.Equals(postalCode), includeProperties: "PostalCode,TaxType");

            var rate = rates.FirstOrDefault(w => w.TaxTypeId == taxType.TaxTypeId
            && amount >= w.From && amount <= w.To);

            amountTaxRate = rate?.Rate ?? 0.0m;

            var taxedAmount = amount * amountTaxRate;
            var amountAfterTax = amount - taxedAmount;

           return  new TaxCalculationViewModel
            {
                TaxType = taxType?.TaxType?.TaxCalculationType,
                AmountAfterTax = amountAfterTax ?? 0.0M,
                TaxPercentage = $"{amountTaxRate * 100}%",
            };

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
                TaxType = rates?.TaxType?.TaxCalculationType,
            };
            return data;
        }

        public TaxCalculationViewModel TaxCalculationLogic(decimal amount, int postalCodeId)
        {
            decimal? amountTaxRate = 0.0M;

            var rates = _unitOfWork.TaxRate.GetAll(includeProperties: "TaxType");

            var taxType = _unitOfWork.PostalCodeTaxType
                .Get(filter: w => w.PostalCode!.Id.Equals(postalCodeId), includeProperties: "PostalCode,TaxType");

            var rate = rates.FirstOrDefault(w => w.TaxTypeId == taxType.TaxTypeId
            && amount >= w.From && amount <= w.To);

            amountTaxRate = rate?.Rate ?? 0.0m;

            var taxedAmount = amount * amountTaxRate;
            var amountAfterTax = amount - taxedAmount;

            return new TaxCalculationViewModel
            {
                TaxType = taxType?.TaxType?.TaxCalculationType,
                AmountAfterTax = amountAfterTax ?? 0.0M,
                TaxPercentage = $"{amountTaxRate * 100}%",
            };
        }
    }
}
