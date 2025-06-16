using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Read
{
    internal sealed class ProductReadConfig: IEntityTypeConfiguration<ProductReadModel>
    {

        public void Configure(EntityTypeBuilder<ProductReadModel> builder)
        {
            builder
                .ToTable("Product")

                .HasKey(x => x.Id);
        }
    }
}
