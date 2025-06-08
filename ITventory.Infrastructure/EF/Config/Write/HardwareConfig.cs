using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class HardwareConfig: IEntityTypeConfiguration<Hardware>
    {
        public void Configure(EntityTypeBuilder<Hardware> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PrimaryUserId)
                .IsRequired();

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.PrimaryUserId);

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.TopUser)
                .IsRequired(false);

            builder.HasMany(x => x.HistoryOfLogons)
                .WithOne()
                .HasForeignKey(l => l.HardwareId);

            builder.Property(x => x.DefaultDomain)
                .HasConversion<string>();

            builder.Property(x => x.HardwareType)
                .HasConversion<string>();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder
                .ToTable("Hardware");

        }
    }
}
