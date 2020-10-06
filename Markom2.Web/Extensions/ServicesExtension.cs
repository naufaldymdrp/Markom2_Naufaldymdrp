using Markom2.Repository.Business.Masters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markom2.Web
{
    public static class MarkomServices
    {
        public static IServiceCollection AddMasterServices(this IServiceCollection services)
        {
            services.AddScoped<MCompanyService>();
            services.AddScoped<MEmployeeService>();

            return services;
        }
    }
}
