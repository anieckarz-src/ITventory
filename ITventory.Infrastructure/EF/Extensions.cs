using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.Repositories;
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

            services.AddScoped<ICountryRepository, PostgresCountryRepository>();
            services.AddScoped<IDepartmentRepository, PostgresDepartmentRepository>();
            services.AddScoped<IEmployeeRepository, PostgresEmployeeRepository>();
            services.AddScoped<IEquipmentRepository, PostgresEquipmentRepository>();
            services.AddScoped<IHardwareRepository, PostgresHardwareRepository>();
            services.AddScoped<ILicenseRepository, PostgresLicenseRepository>();
            services.AddScoped<IModelRepository, PostgresModelRepository>();
            services.AddScoped<IProducentRepository, PostgresProducentRepository>();
            services.AddScoped<IProductRepository, PostgresProductRepository>();
            services.AddScoped<IRoomRepository, PostgresRoomRepository>();
            services.AddScoped<ISoftwareRepository, PostgresSoftwareRepository>();
            services.AddScoped<ILocationRepository, PostgresLocationRepository>();


            var options = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddDbContext<ReadDbContext>(
                ctx => ctx.UseNpgsql(options.ConnectionString));

            services.AddDbContext<WriteDbContext>(
                ctx => ctx.UseNpgsql(options.ConnectionString));

            return services;
        }
    }
}
