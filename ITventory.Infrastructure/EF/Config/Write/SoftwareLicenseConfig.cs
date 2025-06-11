using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Entities.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class SoftwareLicenseConfig : IEntityTypeConfiguration<SoftwareLicense>
    {
        public void Configure(EntityTypeBuilder<SoftwareLicense> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.LicenseKey)
                .HasConversion<string>();

            builder
                .HasOne<SoftwareVersion>()
                .WithMany()
                .HasForeignKey(x => x.SoftwareVersion);

            builder
                .HasMany(x => x.AssignedUsers)
                .WithOne()
                .HasForeignKey("LicenseId");

            builder
               .HasMany(x => x.AssignedHardware)
               .WithOne()
               .HasForeignKey("LicenseId");

            builder
                .ToTable("SoftwareLicense");



        }
    }
}
