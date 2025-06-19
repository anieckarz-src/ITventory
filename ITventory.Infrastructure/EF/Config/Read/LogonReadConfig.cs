using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Read
{
    internal class LogonReadConfig : IEntityTypeConfiguration<LogonReadModel>
    {
        public void Configure(EntityTypeBuilder<LogonReadModel> builder)
        {
            builder
                .ToTable("Logons");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

        }
    }
}
