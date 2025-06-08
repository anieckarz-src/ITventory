using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;

namespace ITventory.Domain
{
    public class Hardware : Item
    {
        public Guid Id { get; private set; }
        public Guid PrimaryUserId { get; private set; }
        public Guid? TopUser =>
         _historyOfLogons
        .GroupBy(l => l.UserId)
        .OrderByDescending(g => g.Count())
        .FirstOrDefault()
        ?.Key;

        public Region DefaultDomain { get; private set; }
        public HardwareType HardwareType { get; init; }
        public bool IsActive { get; private set; }
        public int LoginCount => _historyOfLogons.Count;
        private List<Logon> _historyOfLogons { get; set; }
        public ReadOnlyCollection<Logon> HistoryOfLogons => _historyOfLogons.AsReadOnly();

        private Hardware()
        {

        }

        public Hardware(Guid primaryUserId, Region defaultDomain, HardwareType hardwareType)
        {
            Id = Guid.NewGuid();
            PrimaryUserId = primaryUserId;
            DefaultDomain = defaultDomain;
            HardwareType = hardwareType;
            IsActive = true;
        }

        public static Hardware Create(Guid primaryUserId, Region defaultDomain, HardwareType hardwareType)
        {
            return new Hardware(primaryUserId, defaultDomain, hardwareType);
        }

        public void AddLogon(Logon logon)
        {
            if (logon == null)
            {
                throw new ArgumentNullException(nameof(logon));
            }

            if (_historyOfLogons.Any(l => l.Id == logon.Id))
            {
                throw new ArgumentException("This logon instance already exists");
            }
        }

        public void SetPrimaryUser(Employee user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be empty");
            }

            this.PrimaryUserId = user.Id;

        }
    }
}