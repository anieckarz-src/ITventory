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
    internal sealed class SoftwareConfig : IEntityTypeConfiguration<Software>
    {
        public void Configure(EntityTypeBuilder<Software> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .HasMaxLength(1000);

            builder
                .HasOne<Producent>()
                .WithMany()
                .HasForeignKey(x => x.PublisherId);

            builder
                .Property(x => x.ApprovalType)
                .HasConversion<string>();

            builder
                .HasMany(x => x.SoftwareVersions)
                .WithOne()
                .HasForeignKey("SoftwareId");

            builder
                .ToTable("Software");
        }
    }
}
