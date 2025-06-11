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
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.ManagerId)
                .HasPrincipalKey(x => x.Id);

            builder
                .ToTable("Department");

        }
    }
}
