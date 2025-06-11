using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITventory.Infrastructure.EF.Config.Write
{
    internal sealed class ModelConfig : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.ReleaseDate)
                .HasColumnType("date");
            builder
                .HasOne<Producent>()
                .WithMany()
                .HasForeignKey(x => x.ProducentId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(255);

            builder
                .ToTable("Model");


            builder.HasData(
    new
    {
        Id = Guid.Parse("BFEEE839-38A5-4AA7-89DD-6296F39B1694"),
        Name = "ZH-500",
        ProducentId = Guid.Parse("58EA87F5-103A-4567-8FDA-9B2A32BE2FF9"), // Yamaha
        ReleaseDate = new DateOnly(2016, 5, 2),
        Description = "Top notch technology"
    },
    new
    {
        Id = Guid.Parse("A1122334-5566-7788-99AA-BBCCDDEEFF00"),
        Name = "Xperia Z5",
        ProducentId = Guid.Parse("04DABCE6-41F7-4A66-8C34-963CEDD62A7F"), // Toshiba (Japan)
        ReleaseDate = new DateOnly(2015, 10, 1),
        Description = "Premium smartphone with high-res display"
    },
    new
    {
        Id = Guid.Parse("A2233445-6677-8899-AABB-CCDDEEFF0011"),
        Name = "iPhone 12",
        ProducentId = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67890"), // Apple (US)
        ReleaseDate = new DateOnly(2020, 10, 23),
        Description = "Flagship phone with A14 Bionic chip"
    },
    new
    {
        Id = Guid.Parse("A3344556-7788-99AA-BBCC-DDEEFF001122"),
        Name = "Surface Pro 7",
        ProducentId = Guid.Parse("B2C3D4E5-F678-90A1-B2C3-D4E5F67890A1"), // Microsoft (US)
        ReleaseDate = new DateOnly(2019, 10, 22),
        Description = "Versatile 2-in-1 laptop and tablet"
    },
    new
    {
        Id = Guid.Parse("A4455667-8899-AABB-CCDD-EEFF00112233"),
        Name = "Core i9-11900K",
        ProducentId = Guid.Parse("C3D4E5F6-7890-A1B2-C3D4-E5F67890A1B2"), // Intel (US)
        ReleaseDate = new DateOnly(2021, 3, 16),
        Description = "High-performance desktop CPU"
    },
    new
    {
        Id = Guid.Parse("A5566778-99AA-BBCC-DDEE-FF0011223344"),
        Name = "Pixel 5",
        ProducentId = Guid.Parse("D4E5F678-90A1-B2C3-D4E5-F67890A1B2C3"), // Google (US)
        ReleaseDate = new DateOnly(2020, 10, 15),
        Description = "Google's flagship with clean Android experience"
    },
    new
    {
        Id = Guid.Parse("A6677889-AABB-CCDD-EEFF-001122334455"),
        Name = "Galaxy S21",
        ProducentId = Guid.Parse("E5F67890-A1B2-C3D4-E5F6-7890A1B2C3D4"), // Samsung (South Korea)
        ReleaseDate = new DateOnly(2021, 1, 29),
        Description = "Latest flagship smartphone with Exynos chip"
    },
    new
    {
        Id = Guid.Parse("A778899A-BBCC-DDEE-FF00-112233445566"),
        Name = "LG Velvet",
        ProducentId = Guid.Parse("F67890A1-B2C3-D4E5-F678-90A1B2C3D4E5"), // LG Electronics (South Korea)
        ReleaseDate = new DateOnly(2020, 5, 15),
        Description = "Stylish phone with OLED display"
    },
    new
    {
        Id = Guid.Parse("A8899AAB-CCDD-EEFF-0011-223344556677"),
        Name = "Mate 40 Pro",
        ProducentId = Guid.Parse("7890A1B2-C3D4-E5F6-7890-A1B2C3D4E5F6"), // Huawei (China)
        ReleaseDate = new DateOnly(2020, 10, 22),
        Description = "Flagship phone with cutting-edge camera"
    },
    new
    {
        Id = Guid.Parse("A99AABBC-DDEE-FF00-1122-334455667788"),
        Name = "ThinkPad X1 Carbon Gen 9",
        ProducentId = Guid.Parse("890A1B2C-D4E5-F678-90A1-B2C3D4E5F678"), // Lenovo (China)
        ReleaseDate = new DateOnly(2021, 1, 12),
        Description = "Lightweight business ultrabook"
    },
    new
    {
        Id = Guid.Parse("AAABBCDD-EEFF-0011-2233-445566778899"),
        Name = "XPS 13",
        ProducentId = Guid.Parse("90A1B2C3-D4E5-F678-90A1-B2C3D4E5F678"), // Dell (You could assign if you want - else use another US producent)
        ReleaseDate = new DateOnly(2020, 9, 30),
        Description = "Compact and powerful ultrabook"
    },
    new
    {
        Id = Guid.Parse("BBCCDDEE-FF00-1122-3344-5566778899AA"),
        Name = "SIMATIC S7-1500",
        ProducentId = Guid.Parse("90A1B2C3-D4E5-F678-90A1-B2C3D4E5F678"), // Siemens (Germany)
        ReleaseDate = new DateOnly(2015, 7, 15),
        Description = "Advanced automation controller"
    },
    new
    {
        Id = Guid.Parse("CCDDEEFF-0011-2233-4455-66778899AABB"),
        Name = "PS5",
        ProducentId = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67890"), // Sony (you could add Sony GUID or reassign)
        ReleaseDate = new DateOnly(2020, 11, 12),
        Description = "Next-gen gaming console"
    },
    new
    {
        Id = Guid.Parse("DDEEFF00-1122-3344-5566-778899AABBCC"),
        Name = "Nokia 8.3 5G",
        ProducentId = Guid.Parse("7890A1B2-C3D4-E5F6-7890-A1B2C3D4E5F7"), // Nokia (Finland)
        ReleaseDate = new DateOnly(2020, 9, 15),
        Description = "Mid-range 5G smartphone"
    },
    new
    {
        Id = Guid.Parse("EEFF0011-2233-4455-6677-8899AABBCCDD"),
        Name = "AirPods Pro",
        ProducentId = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67890"), // Apple
        ReleaseDate = new DateOnly(2019, 10, 30),
        Description = "Wireless noise-cancelling earbuds"
    },
    new
    {
        Id = Guid.Parse("FF001122-3344-5566-7788-99AABBCCDDEE"),
        Name = "Razer Blade 15",
        ProducentId = Guid.Parse("A1B2C3D4-E5F6-7890-A1B2-C3D4E5F67890"), // Razer (If you want, else replace)
        ReleaseDate = new DateOnly(2021, 3, 1),
        Description = "High-performance gaming laptop"
    });

        }
    }
}