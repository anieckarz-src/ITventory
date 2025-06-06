using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class License: Entity
    {
        public Guid Id { get; init; }
        public LicenseType LicenseType { get; private set; }
        public string LicenseKey { get; private set; }
        public DateOnly ValidUntil { get; private set; }
        public int MaxUse { get; private set; }
        private int _useCount => _assignedUsers.Count;
        public int UseCount => _useCount;

        private List<Username> _assignedUsers = new();
        public ReadOnlyCollection<Username> AssignedUsers => _assignedUsers.AsReadOnly();

        //zaimplementować licencje na hardware

        private License()
        {

        }

        public License(LicenseType licenseType, string licenseKey, DateOnly validUntil, int maxUse)
        {
            Id = Guid.NewGuid();
            LicenseType = licenseType;
            LicenseKey = licenseKey;
            ValidUntil = validUntil;
            MaxUse = maxUse;
            
        }

        public static License Create(LicenseType licenseType, string licenseKey, DateOnly validUntil, int maxUse)
        {
            return new License(licenseType, licenseKey, validUntil, maxUse);
        }

        public void AssignToUser(Employee user)
        {
            if(LicenseType == LicenseType.PerComputer)
            {
                throw new ArgumentException("You cannot assign hardware license to user");
            }

            if(_assignedUsers.Count >= this.MaxUse)
            {
                throw new InvalidOperationException($"License already used {this.MaxUse} times");
            }

            if(_assignedUsers.Any(u => u.Value == user.Username))
            {
                throw new InvalidOperationException("User already assigned to the license");
            }
            _assignedUsers.Add(user.Username);
        }
    }
}
