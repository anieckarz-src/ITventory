using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext: DbContext
    {

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WriteDbContext).Assembly,
                WriteConfigurationsFilter);
        }

        private static bool WriteConfigurationsFilter(Type type) =>
            type.FullName?.Contains("Config.Read") ?? false;
    }
}
