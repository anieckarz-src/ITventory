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
    internal sealed class SoftwareReadConfig : IEntityTypeConfiguration<SoftwareReadModel>
    {
        public void Configure(EntityTypeBuilder<SoftwareReadModel> builder)
        {
            builder
                .ToTable("Software");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Publisher)
                .WithMany()
                .HasForeignKey(x => x.PublisherId);

            builder
                .HasMany(x => x.Versions)
                .WithOne()
                .HasForeignKey(x => x.SoftwareId);
        }
    }
}
