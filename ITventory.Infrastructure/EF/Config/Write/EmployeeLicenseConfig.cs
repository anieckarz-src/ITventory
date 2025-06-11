using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Entities.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class EmployeeLicenseConfig : IEntityTypeConfiguration<EmployeeLicense>
    {
        public void Configure(EntityTypeBuilder<EmployeeLicense> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder.HasOne<SoftwareLicense>()
                .WithMany()
                .HasForeignKey(e => e.LicenseId);

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.EmployeeId);

            builder
                .ToTable("EmployeeLicense");
        }
    }
}
