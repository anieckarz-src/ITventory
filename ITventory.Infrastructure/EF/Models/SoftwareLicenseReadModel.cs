using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class SoftwareLicenseReadModel
    {
        public Guid Id { get; set; }
        public string LicenseType { get; set; }
        public string LicenseKey { get; set; }
        public DateOnly ValidUntil { get; set; }
        public int MaxUse { get; set; }
        public Guid SoftwareVersion { get; set; }

        public virtual SoftwareVersionReadModel? Software {get;set;}

    }
}
