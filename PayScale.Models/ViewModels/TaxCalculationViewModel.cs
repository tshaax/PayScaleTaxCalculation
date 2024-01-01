using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.Models.ViewModels
{
    public class TaxCalculationViewModel
    {
        public decimal AmountAfterTax { get; set; }
        public string? TaxType { get;set; }

        public string? TaxPercentage { get;set; }
    }
}
