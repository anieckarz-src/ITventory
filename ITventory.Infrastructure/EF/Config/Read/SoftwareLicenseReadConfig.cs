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
    internal sealed class SoftwareLicenseReadConfig : IEntityTypeConfiguration<SoftwareLicenseReadModel>
    {
        public void Configure(EntityTypeBuilder<SoftwareLicenseReadModel> builder)
        {
            builder
                .ToTable("SoftwareLicense");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Software)
                .WithMany()
                .HasForeignKey(x => x.SoftwareVersion);


                
                

        }
    }
}
