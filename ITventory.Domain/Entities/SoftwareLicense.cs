using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Entities.JoinTables;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class SoftwareLicense
    {
        public Guid Id { get; init; }
        public LicenseType LicenseType { get; private set; }
        public string LicenseKey { get; private set; }
        public DateOnly ValidUntil { get; private set; }
        public bool IsExpired() => ValidUntil < DateOnly.FromDateTime(DateTime.Now);
        public int MaxUse { get; private set; }
        public Guid SoftwareVersion { get; private set; }
        public int UseCount => LicenseType switch
        {
            LicenseType.PerUser => AssignedUsers.Count,
            LicenseType.PerComputer => AssignedHardware.Count,
            _ => 0
        };


        public List<EmployeeLicense> AssignedUsers { get; private set; } = new();
  

        public List<HardwareLicense> AssignedHardware { get; private set; } = new();

        private SoftwareLicense()
        {

        }

        public SoftwareLicense(LicenseType licenseType, string licenseKey, DateOnly validUntil, int maxUse, Guid softwareVersion)
        {
            Id = Guid.NewGuid();
            LicenseType = licenseType;
            LicenseKey = licenseKey;
            ValidUntil = validUntil;
            MaxUse = maxUse;
            SoftwareVersion = softwareVersion;
        }

        public static SoftwareLicense Create(LicenseType licenseType, string licenseKey, DateOnly validUntil, int maxUse, Guid softwareVersion)
        {
            return new SoftwareLicense(licenseType, licenseKey, validUntil, maxUse, softwareVersion);
        }

        public void AssignToUser(Employee user)
        {
            if (IsExpired() == true) { throw new InvalidOperationException("License no longer valid"); }

            if(LicenseType == LicenseType.PerComputer)
            {
                throw new ArgumentException("You cannot assign hardware license to user");
            }

            if(UseCount >= this.MaxUse)
            {
                throw new InvalidOperationException($"License already used {this.MaxUse} times");
            }

            if(AssignedUsers.Any(u => u.EmployeeId == user.Id))
            {
                throw new InvalidOperationException("User already assigned to the license");
            }

            var assignment = (EmployeeLicense.Create(this.Id, user.Id));
            AssignedUsers.Add(assignment);
        }

        public void ReassignLicenseToUser(Employee newUser, Employee oldUser)
        {
            if (IsExpired() == true) { throw new InvalidOperationException("License no longer valid"); }

            if (this.LicenseType != LicenseType.PerUser)
            {
                throw new InvalidOperationException("License type other than per user");
            }

            if(newUser == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }

            if(AssignedUsers.Any(u => u.EmployeeId == newUser.Id))
            {
                throw new InvalidOperationException("Cannot reassign the license to the same user");
            }
            if(!AssignedUsers.Any(u => u.EmployeeId == oldUser.Id))
            {
                throw new InvalidOperationException ("The license does not belong to this user");
            }

            AssignedUsers.RemoveAll(u => u.EmployeeId == oldUser.Id);

            AssignedUsers.Add(EmployeeLicense.Create(this.Id, newUser.Id));

        }

        public void AssignToHardware(Hardware hardware)
        {
            if (IsExpired() == true) { throw new InvalidOperationException("License no longer valid"); }

            if (LicenseType != LicenseType.PerComputer)
            {
                throw new ArgumentException("You cannot assign hardware license to user");
            }

            if (UseCount >= this.MaxUse)
            {
                throw new InvalidOperationException($"License already used {this.MaxUse} times");
            }

            if (AssignedHardware.Any(h => h.HardwareId == hardware.Id))
            {
                throw new InvalidOperationException("Hardware already assigned to the license");
            }
            AssignedHardware.Add(HardwareLicense.Create(this.Id, hardware.Id));
        }


        public void ReassignLicenseToHardware(Hardware newHardware, Hardware oldHardware)
        {
            if (IsExpired() == true) { throw new InvalidOperationException("License no longer valid"); }

            if (this.LicenseType != LicenseType.PerComputer)
            {
                throw new InvalidOperationException("License type other than per computer");
            }

            if (newHardware == null)
            {
                throw new ArgumentNullException("Hardware cannot be null");
            }

            if (AssignedHardware.Any(h => h.HardwareId == newHardware.Id))
            {
                throw new InvalidOperationException("Cannot reassign the license to the same hardware");
            }
            if (!AssignedHardware.Any(h=> h.HardwareId == oldHardware.Id))
            {
                throw new InvalidOperationException("The license does not belong to this hardware");
            }

            AssignedHardware.RemoveAll(h => h.HardwareId == oldHardware.Id);

            AssignedHardware.Add(HardwareLicense.Create(this.Id, newHardware.Id));

        }
    }
}
