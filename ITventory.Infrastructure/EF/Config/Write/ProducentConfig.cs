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
    internal sealed class ProducentConfig : IEntityTypeConfiguration<Producent>
    {
        public void Configure(EntityTypeBuilder<Producent> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(200);

            builder
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            builder
                .ToTable("Producent");

        }
    }
}
