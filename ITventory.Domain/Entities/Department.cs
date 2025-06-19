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

        private Department()
        {

        }

        public Department(string name, Guid managerId)
        {
            Id = Guid.NewGuid();
            Name = name;
            ManagerId = managerId;
        }

        public static Department Create(string name, Guid managerId)
        {
            return new Department(name, managerId);
        }

        

        public void SetManager(Guid managerId)
        {
            if (managerId == Guid.Empty) throw new ArgumentNullException("Manager cannot be null");

            this.ManagerId = managerId;
        }


    }
}
