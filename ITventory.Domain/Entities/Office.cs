using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Office: Entity
    {
        public Guid Id { get; init; }
        public string Street { get; private set; }
        public string? BuildingNumber { get; private set; }
        public Guid LocationId {  get; private set; }
        public Latitude Latitude { get; private set; }
        public Longitude Longitude { get; private set; }
        public bool? IsActive { get; private set; }

        private Office()
        {

        }

        public Office(string street, string buildingNumber, Guid locationId, Latitude latitude, Longitude longitude)
        {
            if (String.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentNullException(nameof(street));
            }
            if (String.IsNullOrWhiteSpace(buildingNumber))
            {
                throw new ArgumentNullException(nameof(buildingNumber));
            }

            Id = Guid.NewGuid();
            Street = street;
            BuildingNumber = buildingNumber;
            LocationId = locationId;
            Latitude = latitude;
            Longitude = longitude;

            IsActive = true;
        }

        public static Office Create(string street, string buildingNumber, Guid locationId, Latitude lattitude, Longitude longitude)
        {
            return new Office(street, buildingNumber, locationId, lattitude, longitude);
        }

        public void Deactivate()
        {
            if(this.IsActive == false)
            {
                throw new Exception("Office already in inactive state");
            }

            this.IsActive = false;
        }
    }
}
