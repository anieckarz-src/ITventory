using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ITventory.Infrastructure.EF.AppInit
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddHostedService<AppInitializer>();
            return services;

        }
    }
}
