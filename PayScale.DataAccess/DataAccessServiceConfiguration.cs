using Microsoft.Extensions.DependencyInjection;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.DataAccess.Repository;
using PayScale.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PayScale.DataAccess
{
    public static class DataAccessServiceConfiguration
    {
        public static void AddDataAcessServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseInMemoryDatabase("PayScaleDb"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }   
    }
}
