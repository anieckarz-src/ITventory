using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class SoftwareVersionDTO
    {
        public Guid Id { get; set; }
        public string VersionNumber { get; set; }
        public Guid SoftwareId { get; set; }
        public int Price { get; set; }
        public DateOnly Published { get; set; }
        public bool IsDefault { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public string LicenseType { get; set; }

    }
}
