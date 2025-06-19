using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;


namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class HardwareConfig: IEntityTypeConfiguration<Hardware>
    {
        public void Configure(EntityTypeBuilder<Hardware> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.PrimaryUserId);

            builder
                .HasOne<Producent>()
                .WithMany()
                .HasForeignKey(x => x.ProducentId);

            builder.
               HasOne<Model>()
               .WithMany()
               .HasForeignKey(x => x.ModelId);

            builder
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.RoomId);

            builder
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(x => x.DepartmentId);

            builder
                .Ignore(x => x.TopUser);



            builder.OwnsMany(x => x._historyOfLogons, logonBuilder =>
            {
                logonBuilder.WithOwner().HasForeignKey(l => l.HardwareId);
                logonBuilder.HasKey(l => l.Id); // Required if Logon has an Id

                logonBuilder.Property(x => x.Id)
                .ValueGeneratedNever();

                logonBuilder.Property(x => x.Domain)
                .HasConversion<string>();

                logonBuilder
                    .HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);

                logonBuilder.ToTable("Logons"); // Optional: if stored in a separate table
            });





            builder.Property(x => x.DefaultDomain)
                .HasConversion<string>();

            builder.Property(x => x.HardwareType)
                .HasConversion<string>();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder
                .ToTable("Hardware");

        }
    }
}
