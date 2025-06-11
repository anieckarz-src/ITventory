using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class SoftwareVersionConfig : IEntityTypeConfiguration<SoftwareVersion>
    {
        public void Configure(EntityTypeBuilder<SoftwareVersion> builder)
        {
            builder
                .HasKey(x => x.Id);


            builder
                .Property(x => x.LicenseType)
                .HasConversion<string>();

            builder
                .HasMany(x => x.Reviews)
                .WithOne()
                .HasForeignKey(x => x.SoftwareVersionId);

            builder
                .ToTable("SoftwareVersions");
        }
    }
}
