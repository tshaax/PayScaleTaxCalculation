using PayScale.DataAccess.Repository.IRepository;
using PayScale.DataAcess.Data;
using PayScale.WebRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.DataAccess.Repository
{
    public class TaxCalculationRepository : Repository<TaxCalculation>, ITaxCalculationRepository
    {
        public TaxCalculationRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
