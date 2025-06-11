using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Entities.JoinTables
{
    public class EmployeeLicense
    {

        public Guid Id { get; private set; }
        public Guid LicenseId { get; private set; }
        public Guid EmployeeId { get; private set; }


        public EmployeeLicense(Guid licenseId, Guid employeeId)
        {
            Id = Guid.NewGuid();
            LicenseId = licenseId;
            EmployeeId = employeeId;
        }

        private EmployeeLicense()
        {

        }

        public static EmployeeLicense Create (Guid licenseId, Guid employeeId)
        {
            return new EmployeeLicense(licenseId, employeeId);
        }
        
    }
}
