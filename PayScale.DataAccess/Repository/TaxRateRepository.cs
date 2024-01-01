using PayScale.DataAccess.Repository.IRepository;
using PayScale.DataAcess.Data;
using PayScale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.DataAccess.Repository
{
    public class TaxRateRepository : Repository<TaxRate>, ITaxRateRepository
    {
        public TaxRateRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
