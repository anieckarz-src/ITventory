using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Region)
                .HasConversion<string>();


            builder.HasData(
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0101"), Name = "United States", CountryCode = "US", Region = Region.NorthAmerica },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0102"), Name = "China", CountryCode = "CN", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0103"), Name = "Japan", CountryCode = "JP", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0104"), Name = "Germany", CountryCode = "DE", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0105"), Name = "South Korea", CountryCode = "KR", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0106"), Name = "India", CountryCode = "IN", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0107"), Name = "Taiwan", CountryCode = "TW", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0108"), Name = "Israel", CountryCode = "IL", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0109"), Name = "United Kingdom", CountryCode = "GB", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0110"), Name = "France", CountryCode = "FR", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0111"), Name = "Canada", CountryCode = "CA", Region = Region.NorthAmerica },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0112"), Name = "Singapore", CountryCode = "SG", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0113"), Name = "Netherlands", CountryCode = "NL", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0114"), Name = "Sweden", CountryCode = "SE", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0115"), Name = "Finland", CountryCode = "FI", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0116"), Name = "Australia", CountryCode = "AU", Region = Region.Australia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0117"), Name = "Ireland", CountryCode = "IE", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0118"), Name = "Switzerland", CountryCode = "CH", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0119"), Name = "Belgium", CountryCode = "BE", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120"), Name = "Poland", CountryCode = "PL", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0121"), Name = "Italy", CountryCode = "IT", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0122"), Name = "Spain", CountryCode = "ES", Region = Region.Europe },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0123"), Name = "UAE", CountryCode = "AE", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0124"), Name = "Malaysia", CountryCode = "MY", Region = Region.Asia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0125"), Name = "New Zealand", CountryCode = "NZ", Region = Region.Australia },
     new Country { Id = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0126"), Name = "Norway", CountryCode = "NO", Region = Region.Europe }
 );





        }

    }
}
