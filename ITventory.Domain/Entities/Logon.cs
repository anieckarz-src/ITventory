using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public sealed class Logon: Entity
    {
        public Guid Id { get; init; }
        public Guid HardwareId {  get; init; }
        public Guid UserId { get; init; }
        public Region Domain { get; init; }
        public DateTime LogonTime { get; private set; }
        public string IpAddress {  get; init; }

        public Logon(Guid hardwareId, Guid userId, Region domain, DateTime logonTime, string ipAddress)
        {
            if(!Enum.IsDefined(typeof(Region), domain))
            {
                throw new ArgumentException("Incorrect domain");
            }

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty", nameof(userId));

            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("IpAddress cannot be null or empty", nameof(ipAddress));

            Id = Guid.NewGuid();
            HardwareId = hardwareId;
            UserId = userId;
            Domain = domain;
            LogonTime = logonTime;
            IpAddress = ipAddress;
        }

        public static Logon Create(Guid hardwareId, Guid userId, Region domain, DateTime logonTime, string ipAddress)
        {

            return new Logon(hardwareId, userId, domain,logonTime, ipAddress);

        }
    }
}
