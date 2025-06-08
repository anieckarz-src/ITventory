using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext: DbContext
    {
        public DbSet<Hardware> Hardware { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WriteDbContext).Assembly,
                WriteConfigurationsFilter);
        }

        private static bool WriteConfigurationsFilter(Type type) =>
            type.FullName?.Contains("Config.Write") ?? false;
    }
}
