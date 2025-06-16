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
    internal sealed class LocationReadConfig : IEntityTypeConfiguration<LocationReadModel>
    {
        public void Configure(EntityTypeBuilder<LocationReadModel> builder)
        {

            builder
                .ToTable("Location");

            builder
                .HasKey(x => x.Id);


        }
    }
}
