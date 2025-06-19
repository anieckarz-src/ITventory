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
    internal class OfficeReadConfig : IEntityTypeConfiguration<OfficeReadModel>
    {
        public void Configure(EntityTypeBuilder<OfficeReadModel> builder)
        {
            builder
                .ToTable("Office");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId);

        }
    }
}
