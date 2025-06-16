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
    internal sealed class HardwareConfig : IEntityTypeConfiguration<HardwareReadModel>
    {
        public void Configure(EntityTypeBuilder<HardwareReadModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.PrimaryUser)
                .WithMany()
                .HasForeignKey(x => x.PrimaryUserId);

            builder
                .HasOne(x => x.Producent)
                .WithMany()
                .HasForeignKey(x => x.ProducentId);

            builder
                .HasOne(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId);

            builder
                .HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId);

            builder
                .HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId);

            builder
                .HasMany(x => x.Logons)
                .WithOne()
                .HasForeignKey(x => x.HardwareId);
        }
    }
}
