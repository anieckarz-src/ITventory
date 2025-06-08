using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.Options;
using ITventory.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITventory.Infrastructure.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddDbContext<ReadDbContext>(
                ctx => ctx.UseNpgsql(options.ConnectionString));

            services.AddDbContext<WriteDbContext>(
                ctx => ctx.UseNpgsql(options.ConnectionString));

            return services;
        }
    }
}
