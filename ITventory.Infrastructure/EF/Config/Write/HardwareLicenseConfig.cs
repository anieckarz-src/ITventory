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
    internal sealed class HardwareLicenseConfig : IEntityTypeConfiguration<HardwareLicense>
    {
        public void Configure(EntityTypeBuilder<HardwareLicense> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne<SoftwareLicense>()
                .WithMany()
                .HasForeignKey(e => e.LicenseId);

            builder
                .HasOne<Hardware>()
                .WithMany()
                .HasForeignKey(x => x.HardwareId);

            builder
                .ToTable("HardwareLicense");
        }
    }
}
