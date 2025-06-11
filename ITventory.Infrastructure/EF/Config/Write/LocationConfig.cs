using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class LocationConfig : IEntityTypeConfiguration<Location>
    {

        //Generowanie danych do systemu powierzyliśmy AI
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne<Country>()
                .WithMany()
                .HasForeignKey(e => e.CountryId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(100);

            builder
            .Property(x => x.ZipCode)
            .HasConversion(
                v => v.Value,
                v => new ZipCode(v))
            .HasColumnName("ZipCode")
            .HasMaxLength(20);


            builder
               .Property(x => x.Latitude)
               .HasConversion(
                   v => v.Value,
                   v => new Latitude(v))
               .HasColumnName("Latitude");

            builder
                .Property(x => x.Longitude)
                .HasConversion(
                    v => v.Value,
                    v => new Longitude(v))
                .HasColumnName("Longitude");

            builder
                .Property(x => x.TypeOfPlant)
                .HasConversion<string>();

            builder
                .ToTable("Location");

            builder.HasData(
// Poland (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0120)
new
{
    Id = Guid.Parse("1A2B3C4D-0001-0000-0000-000000000001"),
    Street = "Piłsudskiego 3",
    LocationId = Guid.Parse("1a2b3c4d-0001-0000-0000-000000000001"),
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120"),
    ZipCode = new ZipCode("00-001"),
    Name = "Warsaw AGR East",
    City = "Warsaw",
    Latitude = new Latitude(52.2297),
    Longitude = new Longitude(21.0122),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("1A2B3C4D-0002-0000-0000-000000000002"),
Name = "Krakow Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120"),
ZipCode = new ZipCode("30-001"),
City = "Kraków",
Latitude = new Latitude(50.0647),
Longitude = new Longitude(19.9450),
TypeOfPlant = TypeOfPlant.Warehouse
},
new
{
Id = Guid.Parse("1A2B3C4D-0003-0000-0000-000000000003"),
Name = "Gdansk Other",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120"),
ZipCode = new ZipCode("80-001"),
City = "Gdańsk",
Latitude = new Latitude(54.3520),
Longitude = new Longitude(18.6466),
TypeOfPlant = TypeOfPlant.Other
},
new
{
Id = Guid.Parse("1A2B3C4D-0004-0000-0000-000000000004"),
Name = "Wroclaw Factory",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120"),
ZipCode = new ZipCode("50-001"),
City = "Wrocław",
Latitude = new Latitude(51.1079),
Longitude = new Longitude(17.0385),
TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("1A2B3C4D-0005-0000-0000-000000000005"),
Name = "Poznan Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0120"),
ZipCode = new ZipCode("60-001"),
City = "Poznań",
Latitude = new Latitude(52.4084),
Longitude = new Longitude(16.9342),
TypeOfPlant = TypeOfPlant.Warehouse
},

// Macedonia (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0117)
new
{
    Id = Guid.Parse("2B3C4D5E-0001-0000-0000-000000000006"),
    Name = "Skopje Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0117"),
    ZipCode = new ZipCode("10-00"),
    City = "Skopje",
    Latitude = new Latitude(41.9981),
    Longitude = new Longitude(21.4254),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("2B3C4D5E-0002-0000-0000-000000000007"),
Name = "Bitola Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0117"),
ZipCode = new ZipCode("70-00"),
City = "Bitola",
Latitude = new Latitude(41.0333),
Longitude = new Longitude(21.3333),
TypeOfPlant = TypeOfPlant.Warehouse
},
new
{
Id = Guid.Parse("2B3C4D5E-0003-0000-0000-000000000008"),
Name = "Ohrid Other",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0117"),
ZipCode = new ZipCode("60-00"),
City = "Ohrid",
Latitude = new Latitude(41.1167),
Longitude = new Longitude(20.8000),
TypeOfPlant = TypeOfPlant.Other
},

// Germany (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0104)
new
{
    Id = Guid.Parse("3C4D5E6F-0001-0000-0000-000000000009"),
    Name = "Berlin Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0104"),
    ZipCode = new ZipCode("10-115"),
    City = "Berlin",
    Latitude = new Latitude(52.5200),
    Longitude = new Longitude(13.4050),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("3C4D5E6F-0002-0000-0000-000000000010"),
Name = "Munich Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0104"),
ZipCode = new ZipCode("80-331"),
City = "Munich",
Latitude = new Latitude(48.1351),
Longitude = new Longitude(11.5820),
TypeOfPlant = TypeOfPlant.Warehouse
},

// Netherlands (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0113)
new
{
    Id = Guid.Parse("4D5E6F70-0001-0000-0000-000000000011"),
    Name = "Amsterdam Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0113"),
    ZipCode = new ZipCode("10-12"),
    City = "Amsterdam",
    Latitude = new Latitude(52.3676),
    Longitude = new Longitude(4.9041),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("4D5E6F70-0002-0000-0000-000000000012"),
Name = "Rotterdam Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0113"),
ZipCode = new ZipCode("30-11"),
City = "Rotterdam",
Latitude = new Latitude(51.9225),
Longitude = new Longitude(4.47917),
TypeOfPlant = TypeOfPlant.Warehouse
},

// UK (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0109)
new
{
    Id = Guid.Parse("5E6F7081-0001-0000-0000-000000000013"),
    Name = "London Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0109"),
    ZipCode = new ZipCode("EC1A-1BB"),
    City = "London",
    Latitude = new Latitude(51.5074),
    Longitude = new Longitude(-0.1278),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("5E6F7081-0002-0000-0000-000000000014"),
Name = "Manchester Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0109"),
ZipCode = new ZipCode("M1-1AE"),
City = "Manchester",
Latitude = new Latitude(53.4808),
Longitude = new Longitude(-2.2426),
TypeOfPlant = TypeOfPlant.Warehouse
},

// Italy (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0105)
new
{
    Id = Guid.Parse("6F708192-0001-0000-0000-000000000015"),
    Name = "Rome Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0105"),
    ZipCode = new ZipCode("00-184"),
    City = "Rome",
    Latitude = new Latitude(41.9028),
    Longitude = new Longitude(12.4964),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("6F708192-0002-0000-0000-000000000016"),
Name = "Milan Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0105"),
ZipCode = new ZipCode("20-121"),
City = "Milan",
Latitude = new Latitude(45.4642),
Longitude = new Longitude(9.1900),
TypeOfPlant = TypeOfPlant.Warehouse
},

// France (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0106)
new
{
    Id = Guid.Parse("708192A3-0001-0000-0000-000000000017"),
    Name = "Paris Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0106"),
    ZipCode = new ZipCode("75-001"),
    City = "Paris",
    Latitude = new Latitude(48.8566),
    Longitude = new Longitude(2.3522),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("708192A3-0002-0000-0000-000000000018"),
Name = "Lyon Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0106"),
ZipCode = new ZipCode("69-001"),
City = "Lyon",
Latitude = new Latitude(45.7640),
Longitude = new Longitude(4.8357),
TypeOfPlant = TypeOfPlant.Warehouse
},

// Spain (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0110)
new
{
    Id = Guid.Parse("8192A3B4-0001-0000-0000-000000000019"),
    Name = "Madrid Factory",
    CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0110"),
    ZipCode = new ZipCode("28-001"),
    City = "Madrid",
    Latitude = new Latitude(40.4168),
    Longitude = new Longitude(-3.7038),
    TypeOfPlant = TypeOfPlant.Factory
},
new
{
Id = Guid.Parse("8192A3B4-0002-0000-0000-000000000020"),
Name = "Barcelona Warehouse",
CountryId = Guid.Parse("D61DC6E9-A541-4337-8C4D-7480DFDD0110"),
ZipCode = new ZipCode("08-001"),
City = "Barcelona",
Latitude = new Latitude(41.3851),
Longitude = new Longitude(2.1734),
TypeOfPlant = TypeOfPlant.Warehouse
}
);


        }
    }
}
