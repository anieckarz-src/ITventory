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
    internal class ProductInventoryReadConfig : IEntityTypeConfiguration<ProductInventoryReadModel>
    {
        public void Configure(EntityTypeBuilder<ProductInventoryReadModel> builder)
        {
            builder
                .ToTable("InventoryProduct");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId);

            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);


        }
    }
}
