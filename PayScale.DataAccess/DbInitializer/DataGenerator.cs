using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayScale.DataAcess.Data;
using PayScale.Models;
using System.Reflection.Emit;

namespace PayScale.DataAccess.DbInitializer
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using var context = new ApplicationDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<ApplicationDbContext>>());
                if (context.PostalCode.Any())
                {
                    return; //Data already exists
                }

                context.TaxType.AddRangeAsync(
                    new TaxType { Id = 1, TaxCalculationType = "Progressive" },
                    new TaxType { Id = 2, TaxCalculationType = "Flat Value" },
                    new TaxType { Id = 3, TaxCalculationType = "Flat Rate" });

                context.TaxRate.AddRangeAsync(
    new TaxRate { Id = 1, Rate = 0.1M, From = 0.00M, To = 8350.00M, TaxTypeId = 1 },
    new TaxRate { Id = 2, Rate = 0.15M, From = 8351.00M, To = 33950.00M, TaxTypeId = 1 },
    new TaxRate { Id = 3, Rate = 0.25M, From = 33951.00M, To = 82250.00M, TaxTypeId = 1 },
    new TaxRate { Id = 4, Rate = 0.28M, From = 82251.00M, To = 171550.00M, TaxTypeId = 1 },
    new TaxRate { Id = 5, Rate = 0.33M, From = 171551.00M, To = 372950.00M, TaxTypeId = 1 },
    new TaxRate { Id = 6, Rate = 0.33M, From = 372951.00M, To = 99999999.00M, TaxTypeId = 1 },
    new TaxRate { Id = 7, Rate = 0.05M, From = 833.33M, To = 16666.67M, TaxTypeId = 2 },
    new TaxRate { Id = 8, Rate = 0.17M, From = 0.00M, To = 99999999.00M, TaxTypeId = 3 });

                context.PostalCode.AddRangeAsync(
                    new PostalCode { Id = 1, Code = "7441" },
                    new PostalCode { Id = 2, Code = "A100" },
                    new PostalCode { Id = 3, Code = "7000" },
                new PostalCode { Id = 4, Code = "1000" });


                context.PostalCodeTaxType.AddRangeAsync(
                    new PostalCodeTaxType { Id = 1, PostalCodeId = 1, TaxTypeId = 1 },
                    new PostalCodeTaxType { Id = 2, PostalCodeId = 2, TaxTypeId = 2 },
                    new PostalCodeTaxType { Id = 3, PostalCodeId = 3, TaxTypeId = 3 },
                    new PostalCodeTaxType { Id = 4, PostalCodeId = 4, TaxTypeId = 1 });





                context.SaveChanges();
            }
            catch
            {
                
                throw;
            }


        }

    }
}
