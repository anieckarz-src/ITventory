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
    internal sealed class ProducentConfig : IEntityTypeConfiguration<Producent>
    {
        public void Configure(EntityTypeBuilder<Producent> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(200);

            builder
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            builder
                .ToTable("Producent");

            builder.HasData(
    new Producent
    {
        Id = Guid.Parse("04DABCE6-41F7-4A66-8C34-963CEDD62A7F"),
        Name = "Toshiba",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0103")  // Japan
    },
    new Producent
    {
        Id = Guid.Parse("58EA87F5-103A-4567-8FDA-9B2A32BE2FF9"),
        Name = "Yamaha",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0103")  // Japan (also electronics)
    },
    new Producent
    {
        Id = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67890"),
        Name = "Apple",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0101")  // US
    },
    new Producent
    {
        Id = Guid.Parse("B2C3D4E5-F678-90A1-B2C3-D4E5F67890A1"),
        Name = "Microsoft",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0101")  // US
    },
    new Producent
    {
        Id = Guid.Parse("C3D4E5F6-7890-A1B2-C3D4-E5F67890A1B2"),
        Name = "Intel",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0101")  // US
    },
    new Producent
    {
        Id = Guid.Parse("D4E5F678-90A1-B2C3-D4E5-F67890A1B2C3"),
        Name = "Google",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0101")  // US
    },
    new Producent
    {
        Id = Guid.Parse("E5F67890-A1B2-C3D4-E5F6-7890A1B2C3D4"),
        Name = "Samsung",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0105")  // South Korea
    },
    new Producent
    {
        Id = Guid.Parse("F67890A1-B2C3-D4E5-F678-90A1B2C3D4E5"),
        Name = "LG Electronics",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0105")  // South Korea
    },
    new Producent
    {
        Id = Guid.Parse("7890A1B2-C3D4-E5F6-7890-A1B2C3D4E5F6"),
        Name = "Huawei",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0102")  // China
    },
    new Producent
    {
        Id = Guid.Parse("890A1B2C-D4E5-F678-90A1-B2C3D4E5F678"),
        Name = "Lenovo",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0102")  // China
    },
    new Producent
    {
        Id = Guid.Parse("90A1B2C3-D4E5-F678-90A1-B2C3D4E5F678"),
        Name = "Siemens",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0104")  // Germany
    },
    new Producent
    {
        Id = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67891"),
        Name = "Bosch",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0104")  // Germany
    },
    new Producent
    {
        Id = Guid.Parse("B2C3D4E5-F678-90A1-B2C3-D4E5F67890A2"),
        Name = "Infosys",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0106")  // India
    },
    new Producent
    {
        Id = Guid.Parse("C3D4E5F6-7890-A1B2-C3D4-E5F67890A1B3"),
        Name = "ASUS",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0107")  // Taiwan
    },
    new Producent
    {
        Id = Guid.Parse("D4E5F678-90A1-B2C3-D4E5-F67890A1B2C4"),
        Name = "Acer",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0107")  // Taiwan
    },
    new Producent
    {
        Id = Guid.Parse("E5F67890-A1B2-C3D4-E5F6-7890A1B2C3D5"),
        Name = "Spotify",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0109")  // UK
    },
    new Producent
    {
        Id = Guid.Parse("F67890A1-B2C3-D4E5-F678-90A1B2C3D4E6"),
        Name = "ARM Holdings",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0109")  // UK
    },
    new Producent
    {
        Id = Guid.Parse("7890A1B2-C3D4-E5F6-7890-A1B2C3D4E5F7"),
        Name = "Nokia",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0115")  // Finland
    },
    new Producent
    {
        Id = Guid.Parse("890A1B2C-D4E5-F678-90A1-B2C3D4E5F679"),
        Name = "Ericsson",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0114")  // Sweden
    },
    new Producent
    {
        Id = Guid.Parse("90A1B2C3-D4E5-F678-90A1-B2C3D4E5F680"),
        Name = "SAP",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0104")  // Germany
    },
    new Producent
    {
        Id = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67892"),
        Name = "Philips",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0113")  // Netherlands
    },
    new Producent
    {
        Id = Guid.Parse("B2C3D4E5-F678-90A1-B2C3-D4E5F67890A3"),
        Name = "Comarch",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120")  // Poland
    },
    new Producent
    {
        Id = Guid.Parse("C3D4E5F6-7890-A1B2-C3D4-E5F67890A1B4"),
        Name = "Dassault Systèmes",
        CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0110")  // France
    });






        }
    }
}
