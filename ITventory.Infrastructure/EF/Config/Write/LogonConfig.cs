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
    internal sealed class LogonConfig : IEntityTypeConfiguration<Logon>
    {
        public void Configure(EntityTypeBuilder<Logon> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Domain)
                .HasConversion<string>();

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder
                .ToTable("Logon");

        }
    }
}
