using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class SoftwareLicenseDTO
    {
        public Guid Id { get; set; }
        public string LicenseType { get; set; }
        public string LicenseKey { get; set; }
        public DateOnly ValidUntil { get; set; }
        public int MaxUse { get; set; }
        public Guid SoftwareVersion { get; set; }

        public SoftwareVersionDTO Software { get; set; }
    }
}
