using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
       

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var usernameConverter = new ValueConverter<Username, string>(
             username => username.Value,        // To DB conversion
             value => new Username(value));    // From DB conversion


            builder.HasKey(x => x.Id);

            builder.Property(x => x.IdentityId)
                .IsRequired();

            //builder.ComplexProperty(x => x.Username);

            //builder.Property(x => x.Username).
            //HasConversion(x => x.Value, x => new Username(x));

            builder.Property(e => e.Username)
                .HasConversion(usernameConverter)
                .HasColumnName("Username");
                



            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Area)
                .HasConversion<string>()
                .IsRequired(false);


            builder.Property(x => x.Seniority)
            .HasConversion<string>();

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.ManagerId);

            builder
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(x => x.DepartmentId);



            builder
                .ToTable("Employee");

        }
    }
}
