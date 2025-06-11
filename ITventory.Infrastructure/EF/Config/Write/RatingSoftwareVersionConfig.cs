using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class RatingSoftwareVersionConfig : IEntityTypeConfiguration<RatingSoftwareVersion>
    {
        public void Configure(EntityTypeBuilder<RatingSoftwareVersion> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ReviewedProductId);

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.RatedById);

            builder
                .HasOne<SoftwareVersion>()
                .WithMany()
                .HasForeignKey(x => x.SoftwareVersionId);

            builder
                .ToTable("RatingSoftwareVersion");
        }
    }
}
