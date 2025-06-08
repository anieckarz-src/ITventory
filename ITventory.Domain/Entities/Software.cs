using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Software: Entity
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public Guid PublisherId { get; private set; }
        public SoftwareVersion DefaultVersion => _softwareVersions.FirstOrDefault(v => v.IsDefault);

        public ApprovalType ApprovalType { get; private set; }

        private readonly List<SoftwareVersion> _softwareVersions = new();
        public IReadOnlyCollection<SoftwareVersion> SoftwareVersions => _softwareVersions.AsReadOnly();

        private Software()
        {

        }

        public Software(Guid publisherId, ApprovalType approvalType, string name)
        {
            if(String.IsNullOrWhiteSpace(name) || name.Length < 2)
            {
                throw new ArgumentException("Invalid software name");
            }

            Id = Guid.NewGuid();
            PublisherId = publisherId;
            ApprovalType = approvalType;
            Name = name;
        }

        public static Software Create (Guid publisherId, ApprovalType approvalType, string name)
        {
            return new Software(publisherId, approvalType, name);
        }

        public void SetDefaultVersion(Guid versionNumberId)
        {
            var contemporaryDefault = SoftwareVersions.FirstOrDefault(d => d.IsDefault == true);

            var version = SoftwareVersions.FirstOrDefault(v => v.Id == versionNumberId);
            {
                if(version is null)
                {
                    throw new InvalidOperationException("Version not found");
                }

                //dodać logikę w application, żeby nazwy wersji były unikalne
                contemporaryDefault?.RemoveDefault();
                version.MakeDefault();
            }
        }

        public void AddVersion(SoftwareVersion version)
        {
            if(_softwareVersions.Any(v => v.VersionNumber == version.VersionNumber)) {
                throw new InvalidOperationException("This software version already exists");
            }
            _softwareVersions.Add(version);
        }
    }
}
