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
    internal class ModelReadConfig : IEntityTypeConfiguration<ModelReadModel>
    {
        public void Configure(EntityTypeBuilder<ModelReadModel> builder)
        {
            builder
                .ToTable("Model");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Producent)
                .WithMany()
                .HasForeignKey(x => x.ProducentId);
            

        }
    }
}
