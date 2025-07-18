﻿    using System;
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
            public DbSet<ProducentReadModel> Producents { get; set; }
            public DbSet<ModelReadModel> Models { get; set; }
            public DbSet<ProductReadModel> Products { get; set; }
            public DbSet<DepartmentReadModel> Department { get; set; }
            public DbSet<EmployeeReadModel> Employee { get; set; }

            public DbSet<RoomReadModel> Rooms { get; set; }
            public DbSet<HardwareReadModel> Hardware { get; set; }
            public DbSet<LogonReadModel> Logon { get; set; }
            public DbSet<EquipmentReadModel> Equipment { get; set; }
            public DbSet<ProductInventoryReadModel> ProductInventory { get; set; }
            public DbSet<OfficeReadModel> Office { get; set; }
            public DbSet<SoftwareReadModel> Software { get; set; }
            public DbSet<SoftwareVersionReadModel> SoftwareVersion { get; set; }
            public DbSet<SoftwareLicenseReadModel> SoftwareLicense { get; set; }
        

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
