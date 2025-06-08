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
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Hardware> Hardware { get; set; }
        public DbSet<SoftwareLicense> SoftwareLicense { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Producent> Producents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Software> Software { get; set; }


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
