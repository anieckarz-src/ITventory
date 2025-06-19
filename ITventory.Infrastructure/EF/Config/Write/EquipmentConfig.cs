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


            builder.OwnsMany(x => x.HistoryOfReviews, logonBuilder =>
            {
                logonBuilder.WithOwner().HasForeignKey(l => l.ReviwedEquipmentId);
                logonBuilder.HasKey(l => l.Id);

                logonBuilder.Property(l => l.Id).ValueGeneratedNever();

                logonBuilder.Property(l => l.Condition).HasConversion<string>().HasColumnType("text");


                logonBuilder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(l => l.ReviewerId);


            });

            builder
                .ToTable("Equipment");
            
                
        }
    }
}
