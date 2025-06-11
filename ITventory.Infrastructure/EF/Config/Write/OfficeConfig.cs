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

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class OfficeConfig : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {


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
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Street)
                .HasMaxLength(100);

            builder
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(x => x.LocationId);

            builder.HasData(
            // Poland (CountryId: D61DC6E9-A541-4337-8C4D-7480DFDD0120)
            new
            {
                Id = Guid.Parse("F1A2B3C4-0001-0000-0000-000000000001"),
                Street = "Marszałkowska 5",
                BuildingNumber = "3A",
                LocationId = Guid.Parse("1a2b3c4d-0001-0000-0000-000000000001"), // Warsaw
                Latitude = new Latitude(52.2297),
                Longitude = new Longitude(21.0122),
                IsActive = true
            },
new
{
    Id = Guid.Parse("F1A2B3C4-0002-0000-0000-000000000002"),
    Street = "Krakowska 12",
    BuildingNumber = "5B",
    LocationId = Guid.Parse("1a2b3c4d-0002-0000-0000-000000000002"), // Krakow
    Latitude = new Latitude(50.0647),
    Longitude = new Longitude(19.9450),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0003-0000-0000-000000000003"),
    Street = "Długa 7",
    BuildingNumber = "1C",
    LocationId = Guid.Parse("1a2b3c4d-0003-0000-0000-000000000003"), // Gdansk
    Latitude = new Latitude(54.3520),
    Longitude = new Longitude(18.6466),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0004-0000-0000-000000000004"),
    Street = "Rynek 1",
    BuildingNumber = "2",
    LocationId = Guid.Parse("1a2b3c4d-0004-0000-0000-000000000004"), // Wroclaw
    Latitude = new Latitude(51.1079),
    Longitude = new Longitude(17.0385),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0005-0000-0000-000000000005"),
    Street = "Stary Rynek 10",
    BuildingNumber = "A",
    LocationId = Guid.Parse("1a2b3c4d-0005-0000-0000-000000000005"), // Poznan
    Latitude = new Latitude(52.4084),
    Longitude = new Longitude(16.9342),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0006-0000-0000-000000000006"),
    Street = "Main Street 10",
    BuildingNumber = "1",
    LocationId = Guid.Parse("2b3c4d5e-0001-0000-0000-000000000006"), // Skopje
    Latitude = new Latitude(41.9981),
    Longitude = new Longitude(21.4254),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0007-0000-0000-000000000007"),
    Street = "Bitola Blvd 45",
    BuildingNumber = "12",
    LocationId = Guid.Parse("2b3c4d5e-0002-0000-0000-000000000007"), // Bitola
    Latitude = new Latitude(41.0333),
    Longitude = new Longitude(21.3333),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0008-0000-0000-000000000008"),
    Street = "Ohrid Lakeside 3",
    BuildingNumber = "4C",
    LocationId = Guid.Parse("2b3c4d5e-0003-0000-0000-000000000008"), // Ohrid
    Latitude = new Latitude(41.1167),
    Longitude = new Longitude(20.8000),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0009-0000-0000-000000000009"),
    Street = "Alexanderplatz 2",
    BuildingNumber = "10",
    LocationId = Guid.Parse("3c4d5e6f-0001-0000-0000-000000000009"), // Berlin
    Latitude = new Latitude(52.5200),
    Longitude = new Longitude(13.4050),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0010-0000-0000-000000000010"),
    Street = "Marienplatz 1",
    BuildingNumber = "7B",
    LocationId = Guid.Parse("3c4d5e6f-0002-0000-0000-000000000010"), // Munich
    Latitude = new Latitude(48.1351),
    Longitude = new Longitude(11.5820),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0011-0000-0000-000000000011"),
    Street = "Damrak 20",
    BuildingNumber = "5A",
    LocationId = Guid.Parse("4d5e6f70-0001-0000-0000-000000000011"), // Amsterdam
    Latitude = new Latitude(52.3676),
    Longitude = new Longitude(4.9041),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0012-0000-0000-000000000012"),
    Street = "Coolsingel 100",
    BuildingNumber = "2",
    LocationId = Guid.Parse("4d5e6f70-0002-0000-0000-000000000012"), // Rotterdam
    Latitude = new Latitude(51.9225),
    Longitude = new Longitude(4.47917),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0013-0000-0000-000000000013"),
    Street = "Baker Street 221B",
    BuildingNumber = "1",
    LocationId = Guid.Parse("5e6f7081-0001-0000-0000-000000000013"), // London
    Latitude = new Latitude(51.5074),
    Longitude = new Longitude(-0.1278),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0014-0000-0000-000000000014"),
    Street = "Deansgate 50",
    BuildingNumber = "5",
    LocationId = Guid.Parse("5e6f7081-0002-0000-0000-000000000014"), // Manchester
    Latitude = new Latitude(53.4808),
    Longitude = new Longitude(-2.2426),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0015-0000-0000-000000000015"),
    Street = "Via del Corso 15",
    BuildingNumber = "6",
    LocationId = Guid.Parse("6f708192-0001-0000-0000-000000000015"), // Rome
    Latitude = new Latitude(41.9028),
    Longitude = new Longitude(12.4964),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0016-0000-0000-000000000016"),
    Street = "Via Monte Napoleone 20",
    BuildingNumber = "3",
    LocationId = Guid.Parse("6f708192-0002-0000-0000-000000000016"), // Milan
    Latitude = new Latitude(45.4642),
    Longitude = new Longitude(9.1900),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0017-0000-0000-000000000017"),
    Street = "Rue de Rivoli 10",
    BuildingNumber = "8",
    LocationId = Guid.Parse("708192a3-0001-0000-0000-000000000017"), // Paris
    Latitude = new Latitude(48.8566),
    Longitude = new Longitude(2.3522),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0018-0000-0000-000000000018"),
    Street = "Rue Mercière 12",
    BuildingNumber = "4",
    LocationId = Guid.Parse("708192a3-0002-0000-0000-000000000018"), // Lyon
    Latitude = new Latitude(45.7640),
    Longitude = new Longitude(4.8357),
    IsActive = true
},

new
{
    Id = Guid.Parse("F1A2B3C4-0019-0000-0000-000000000019"),
    Street = "Gran Via 50",
    BuildingNumber = "7",
    LocationId = Guid.Parse("8192a3b4-0001-0000-0000-000000000019"), // Madrid
    Latitude = new Latitude(40.4168),
    Longitude = new Longitude(-3.7038),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0020-0000-0000-000000000020"),
    Street = "Passeig de Gràcia 30",
    BuildingNumber = "9",
    LocationId = Guid.Parse("8192a3b4-0002-0000-0000-000000000020"), // Barcelona
    Latitude = new Latitude(41.3851),
    Longitude = new Longitude(2.1734),
    IsActive = true
},

// 10 more offices repeating some locations with different street/building numbers
new
{
    Id = Guid.Parse("F1A2B3C4-0021-0000-0000-000000000021"),
    Street = "Nowy Świat 20",
    BuildingNumber = "5",
    LocationId = Guid.Parse("1a2b3c4d-0001-0000-0000-000000000001"), // Warsaw again
    Latitude = new Latitude(52.2297),
    Longitude = new Longitude(21.0122),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0022-0000-0000-000000000022"),
    Street = "Kazimierza Wielkiego 8",
    BuildingNumber = "2",
    LocationId = Guid.Parse("1a2b3c4d-0004-0000-0000-000000000004"), // Wroclaw
    Latitude = new Latitude(51.1079),
    Longitude = new Longitude(17.0385),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0023-0000-0000-000000000023"),
    Street = "Chmielna 10",
    BuildingNumber = "3",
    LocationId = Guid.Parse("1a2b3c4d-0003-0000-0000-000000000003"), // Gdansk
    Latitude = new Latitude(54.3520),
    Longitude = new Longitude(18.6466),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0024-0000-0000-000000000024"),
    Street = "Krakowska 45",
    BuildingNumber = "6",
    LocationId = Guid.Parse("1a2b3c4d-0002-0000-0000-000000000002"), // Krakow
    Latitude = new Latitude(50.0647),
    Longitude = new Longitude(19.9450),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0025-0000-0000-000000000025"),
    Street = "Plac Wolności 5",
    BuildingNumber = "1",
    LocationId = Guid.Parse("1a2b3c4d-0005-0000-0000-000000000005"), // Poznan
    Latitude = new Latitude(52.4084),
    Longitude = new Longitude(16.9342),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0026-0000-0000-000000000026"),
    Street = "Skopje Center 3",
    BuildingNumber = "7",
    LocationId = Guid.Parse("2b3c4d5e-0001-0000-0000-000000000006"), // Skopje
    Latitude = new Latitude(41.9981),
    Longitude = new Longitude(21.4254),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0027-0000-0000-000000000027"),
    Street = "Berlin Wall Str 15",
    BuildingNumber = "8",
    LocationId = Guid.Parse("3c4d5e6f-0001-0000-0000-000000000009"), // Berlin
    Latitude = new Latitude(52.5200),
    Longitude = new Longitude(13.4050),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0028-0000-0000-000000000028"),
    Street = "London Bridge 22",
    BuildingNumber = "9",
    LocationId = Guid.Parse("5e6f7081-0001-0000-0000-000000000013"), // London
    Latitude = new Latitude(51.5074),
    Longitude = new Longitude(-0.1278),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0029-0000-0000-000000000029"),
    Street = "Via Roma 100",
    BuildingNumber = "4",
    LocationId = Guid.Parse("6f708192-0001-0000-0000-000000000015"), // Rome
    Latitude = new Latitude(41.9028),
    Longitude = new Longitude(12.4964),
    IsActive = true
},
new
{
    Id = Guid.Parse("F1A2B3C4-0030-0000-0000-000000000030"),
    Street = "Rue Lafayette 10",
    BuildingNumber = "3",
    LocationId = Guid.Parse("708192a3-0001-0000-0000-000000000017"), // Paris
    Latitude = new Latitude(48.8566),
    Longitude = new Longitude(2.3522),
    IsActive = true
}

);


        }
    }
}
