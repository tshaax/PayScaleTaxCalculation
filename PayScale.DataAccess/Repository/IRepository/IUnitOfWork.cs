using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITaxRateRepository TaxRate { get; }
        IPostalCodeTaxTypeRepository PostalCodeTaxType { get; }
        ITaxTypeRepository TaxType { get; }
        ITaxCalculationRepository TaxCalculation { get; }

        void Save();
    }
}
