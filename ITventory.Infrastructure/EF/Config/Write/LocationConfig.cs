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
    internal sealed class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(e => e.CountryId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(100);

            builder
                .OwnsOne(z => z.ZipCode);

            builder
                .OwnsOne(x => x.Latitude);

            builder
                .OwnsOne(x => x.Longitude);

            builder
                .Property(x => x.TypeOfPlant)
                .HasConversion<string>();

            builder
                .ToTable("Location");
        }
    }
}
