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
    internal sealed class OfficeConfig : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Street)
                .HasMaxLength(100);

            builder
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.LocationId);

            builder
                .OwnsOne(x => x.Lattitude);

            builder
                .OwnsOne(x => x.Longitude);

            builder
                .ToTable("Office");
        }
    }
}
