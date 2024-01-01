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
    public class TaxTypeRepository : Repository<TaxType>, ITaxTypeRepository
    {
        public TaxTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
