using Microsoft.Extensions.DependencyInjection;
using PayScale.BusinessLayer.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.BusinessLayer
{
    public static class BusinessLayerServicesConfiguration
    {
        public static void AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IBusinessLogic,BusinessLogic>();
        }
    }
}
