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
    internal sealed class EquipmentReadConfig : IEntityTypeConfiguration<EquipmentReadModel>
    {
        public void Configure(EntityTypeBuilder<EquipmentReadModel> builder)
        {
            builder
                .HasKey(x => x.Id);

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
        }
    }
}
