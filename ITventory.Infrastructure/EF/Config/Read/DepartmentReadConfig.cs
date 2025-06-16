using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Read
{
    internal class DepartmentReadConfig : IEntityTypeConfiguration<DepartmentReadModel>
    {
        public void Configure(EntityTypeBuilder<DepartmentReadModel> builder)
        {

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Manager)
                .WithMany()
                .HasForeignKey(x => x.ManagerId);
        }
    }
}
