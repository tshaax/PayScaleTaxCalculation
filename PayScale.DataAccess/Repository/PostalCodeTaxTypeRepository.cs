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
    public class PostalCodeTaxTypeRepository : Repository<PostalCodeTaxType>, IPostalCodeTaxTypeRepository
    {
        public PostalCodeTaxTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
