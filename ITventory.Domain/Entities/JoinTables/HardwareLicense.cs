using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Entities.JoinTables
{
    public class HardwareLicense
    {

        public Guid Id { get; private set; }
        public Guid LicenseId { get; private set; }
        public Guid HardwareId { get; private set; }

        public HardwareLicense(Guid licenseId, Guid hardwareId)
        {
            Id = Guid.NewGuid();
            LicenseId = licenseId;
            HardwareId = hardwareId;
        }

        private HardwareLicense()
        {

        }

        public static HardwareLicense Create(Guid licenseId, Guid hardwareId)
        {
            return new HardwareLicense(licenseId, hardwareId);
        }

    }
}
