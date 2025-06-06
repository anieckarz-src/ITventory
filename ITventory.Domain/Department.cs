using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Department: Entity
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid ManagerId { get; private set; }
        private List<Software> _recommendedSoftware = new();
        public IReadOnlyCollection<Software> RecommendedSoftware => _recommendedSoftware.AsReadOnly();


        private Department()
        {

        }

        public Department(string name, Guid managerId)
        {
            Id = Guid.NewGuid();
            Name = name;
            ManagerId = managerId;
        }
        public void AssignRecommendedSoftware(Software software)
        {
            if(software == null)
            {
                throw new ArgumentException("Software is null here");
            }
            if(_recommendedSoftware.Any(s => s.Name == software.Name))
            {
                throw new ArgumentException("Softwre already added to recommended list");
            }

            _recommendedSoftware.Add(software);
        }

        public void UpdateRecommendedSoftwareList(List<Software> softwareList)
        {
            if (softwareList == null) return;

            foreach (var software in softwareList)
            {
                if (software == null) continue;

                if (_recommendedSoftware.Any(s => s.Name == software.Name))
                    continue;

                _recommendedSoftware.Add(software);
            }
        }

        public void SetManager(Guid managerId)
        {
            if (managerId == null) throw new ArgumentNullException("Manager cannot be null");

            this.ManagerId = managerId;
        }


    }
}
