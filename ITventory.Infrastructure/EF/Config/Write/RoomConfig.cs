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
    internal sealed class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Office>()
                .WithMany()
                .HasForeignKey(x => x.OfficeId);

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.PersonResponsibleId);

            builder
                .HasMany(x => x.Employees)
                .WithOne()
                .HasForeignKey("RoomId");

            builder
                .HasMany(x => x.RoomInventory)
                .WithOne()
                .HasForeignKey("RoomId");

            builder
                .ToTable("Rooms");

        }
    }
}
