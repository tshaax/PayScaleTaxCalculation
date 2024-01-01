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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public ITaxRateRepository TaxRate { get; private set; }

        public IPostalCodeTaxTypeRepository PostalCodeTaxType { get; private set; }

        public ITaxTypeRepository TaxType { get; private set; }

        public ITaxCalculationRepository TaxCalculation { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            PostalCodeTaxType = new PostalCodeTaxTypeRepository(_db);
            TaxRate = new TaxRateRepository(_db);
            TaxType = new TaxTypeRepository(_db);
            TaxCalculation = new TaxCalculationRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
