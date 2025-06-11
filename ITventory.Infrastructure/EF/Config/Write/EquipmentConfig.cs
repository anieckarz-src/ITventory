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
    internal sealed class EquipmentConfig : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Producent>()
                .WithMany()
                .HasForeignKey(x => x.ProducentId);

            builder.
                HasOne<Model>()
                .WithMany()
                .HasForeignKey(x => x.ModelId);

            builder.
                HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.RoomId);

            builder.
                HasOne<Department>()
                .WithMany()
                .HasForeignKey(x => x.DepartmentId);

            builder.
                Property(x => x.Condition)
                .HasConversion<string>();

            builder
                .HasMany(x => x.HistoryOfReviews)
                .WithOne()
                .HasForeignKey(r => r.ReviwedEquipmentId);

            builder
                .ToTable("Equipment");
            
                
        }
    }
}
