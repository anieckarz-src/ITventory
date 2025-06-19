using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Entities;
using ITventory.Domain.Enums;

namespace ITventory.Domain
{
    public class SoftwareVersion
    {
        public Guid Id { get; init; }
        public string VersionNumber { get; init; }
        public Guid SoftwareId { get; private set; }
        public decimal Price { get; private set; }
        public DateOnly Published { get; private set; }
        public bool IsDefault { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsActive { get; private set; }
        public LicenseType LicenseType { get; private set; }

        public List<RatingSoftwareVersion> Reviews { get; private set; } = new();

        private SoftwareVersion()
        {

        }

        public SoftwareVersion(Guid softwareId, string versionNumber, decimal price, DateOnly published, LicenseType licenseType)
        {
            if (!versionNumber.Contains('.') || versionNumber.Length < 2)
            {
                throw new ArgumentException("Version must be at least 2 character long and contains a dot");
            }

            if(price < 0 || price > 99999)
            {
                throw new ArgumentException("Invalid price");
            }
            if(published > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentException("Published date from future");
            }


            Id = Guid.NewGuid();
            VersionNumber = versionNumber;
            SoftwareId = softwareId;
            Price = price;
            Published = published;
            LicenseType = licenseType;
            IsDefault = false;
            IsApproved = false;
            IsActive = false;
        }

        public static SoftwareVersion Create(Guid softwareId, string versionNumber, decimal price, DateOnly published, LicenseType licenseType)
        {
            return new SoftwareVersion(softwareId, versionNumber, price, published, licenseType);
        }

        public void ApproveSoftware()
        {
            this.IsApproved = true;
            this.IsActive = true;
        }

        public void RetireSoftware()
        {
            this.IsActive = false;
        }

        internal protected void MakeDefault()
        {
            this.IsDefault = true;
        }
        internal protected void RemoveDefault()
        {
            this.IsDefault = false;
        }


        public void ChangePrice(decimal price)
        {
            if (price < 0 || price > 99999)
            {
                throw new ArgumentException("Invalid price");
            }

            Price = price;
        }

        public void AddRating(RatingSoftwareVersion rating)
        {
            Reviews.Add(rating);
        }

    }
}
