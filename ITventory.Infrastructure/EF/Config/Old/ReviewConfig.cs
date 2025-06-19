using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Old
{
    internal sealed class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.ReviewerId);

            builder
                .Property(x => x.Condition)
                .HasConversion<string>();

            builder
                .ToTable("Review");
        }
    }
}
