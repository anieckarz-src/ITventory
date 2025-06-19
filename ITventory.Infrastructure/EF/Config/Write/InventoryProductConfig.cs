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
    internal sealed class InventoryProductConfig : IEntityTypeConfiguration<InventoryProduct>
    {
        public void Configure(EntityTypeBuilder<InventoryProduct> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.RoomId);


            builder
                .ToTable("InventoryProduct");
        }
    }
}
