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

            builder
                .HasData(
                new
                {
                    Id = Guid.Parse("8F5F3D88-CB83-4748-9D93-191B99B903CC"),
                    OfficeId = Guid.Parse("f1a2b3c4-0001-0000-0000-000000000001"),
                    Floor = 3,
                    Area = 1149.00,
                    Capacity = 100,
                    PersonResponsibleId = Guid.Parse("7ebc5231-ae71-4c64-8154-ffe53c88cd0c")

                }
                );
            
        }
    }
}
