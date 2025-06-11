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
    internal sealed class ModelConfig : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Producent>()
                .WithMany()
                .HasForeignKey(x => x.ProducentId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(255);

            builder
                .ToTable("Model");
        }
    }
}
