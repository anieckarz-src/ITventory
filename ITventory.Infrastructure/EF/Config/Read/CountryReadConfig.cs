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
    internal sealed class CountryReadConfig : IEntityTypeConfiguration<CountryReadModel>
    {
        public void Configure(EntityTypeBuilder<CountryReadModel> builder)
        {
            builder
                .ToTable("Countries");

            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(x => x.Locations)
                .WithOne()
                .HasForeignKey(c => c.CountryId);
                
        }
    }
}
