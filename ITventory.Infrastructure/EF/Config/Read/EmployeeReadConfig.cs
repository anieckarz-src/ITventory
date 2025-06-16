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
    internal class EmployeeReadConfig : IEntityTypeConfiguration<EmployeeReadModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeReadModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Manager)
                .WithMany()
                .HasForeignKey(x => x.ManagerId);

            builder
                .HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId);

            builder
                .HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId);

        }
    }
}
