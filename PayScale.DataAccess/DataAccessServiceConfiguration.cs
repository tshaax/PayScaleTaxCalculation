using Microsoft.Extensions.DependencyInjection;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.DataAccess.Repository;
using PayScale.DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PayScale.DataAccess
{
    public static class DataAccessServiceConfiguration
    {
        public static void AddDataAcessServices(this IServiceCollection services, string? sqlConnection )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(sqlConnection));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }   
    }
}
