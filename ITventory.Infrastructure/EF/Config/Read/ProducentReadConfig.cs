using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Read
{
    internal sealed class ProducentReadConfig : IEntityTypeConfiguration<ProducentReadModel>
    {
        public void Configure(EntityTypeBuilder<ProducentReadModel> builder)
        {

            builder
                .ToTable("Producent");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryId);

            builder
                .HasMany(x => x.Models)
                .WithOne(x => x.Producent)
                .HasForeignKey(x => x.ProducentId);

            builder
                .HasMany(x => x.Software)
                .WithOne(x => x.Publisher)
                .HasForeignKey(x => x.PublisherId);
        }
    }
}
