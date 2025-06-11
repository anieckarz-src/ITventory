using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext: DbContext
    {

        public DbSet<CountryReadModel> Countries { get; set; }
        public DbSet<LocationReadModel> Locations { get; set; }


        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ReadDbContext).Assembly,
                WriteConfigurationsFilter);
        }

        private static bool WriteConfigurationsFilter(Type type) =>
            type.FullName?.Contains("Config.Read") ?? false;
    }
}
