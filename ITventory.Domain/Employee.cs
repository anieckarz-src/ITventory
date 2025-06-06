using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Employee: Entity
    {
        public Guid Id { get; init; }
        public Username Username { get; init; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public bool IsActive { get; private set; }
        public Area Area { get; private set; }
        public string PositionName { get; private set; }
        public Seniority Seniority { get; private set; }
        public Guid ManagerId { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Guid Office { get; private set; }
        public DateOnly HireDate { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public int Experience => DateOnly.FromDateTime(DateTime.UtcNow).Year - HireDate.Year;


        private Employee()
        {

        }

        public Employee(Username username, string name, string lastName, Area area, string positionName, Seniority seniority, Guid managerId, Guid departmentId, Guid ofifceId, DateOnly hireDate, DateOnly birthDate)
        {

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Namee is empty");
            }

            if (String.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException("Name is empty");
            }

            if (String.IsNullOrWhiteSpace(positionName) || positionName.Length < 3)
            {
                throw new ArgumentNullException("Namee is empty or it's to shoort - it must contain at least 3 characters");
            }

            if(!Enum.IsDefined(typeof(Seniority), seniority))
            {
                throw new ArgumentException("Incorrect seniority");
            }

            if (hireDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentNullException("Future hire date");
            }


            Id = Guid.NewGuid();
            Username = username;
            Name = name;
            LastName = lastName;
            Area = area;
            PositionName = positionName;
            Seniority = seniority;
            ManagerId = managerId;
            DepartmentId = departmentId;
            Office = ofifceId;
            HireDate = hireDate;
            BirthDate = birthDate;
            IsActive = true;
        }

        public static Employee Create(Username username, string name, string lastName, Area area, string positionName,
                                  Seniority seniority, Guid managerId, Guid departmentId, Guid officeId, DateOnly hireDate,
                                  DateOnly birthDate)
        {
            return new Employee(username, name, lastName, area, positionName, seniority, managerId, departmentId, officeId, hireDate, birthDate);
        }

        public void SetManager(Guid managerId)
        {
            if(managerId == Guid.Empty || managerId == this.Id)
            {
                throw new ArgumentException("Manager ID cannot be empty or the same as the current user");
            }

            this.ManagerId = managerId;
        }

        public void ChangeDepartment(Guid departmentId)
        {
            if (this.DepartmentId == departmentId)
            {
                throw new ArgumentException("Cannot set department to the same one");
            }
            this.DepartmentId = departmentId;
        }
        
        public void Promote(Seniority seniority)
        {
            if(this.Seniority == seniority)
            {
                throw new ArgumentException("Cannot set seniority to the same one");
            }
            this.Seniority = seniority;
        }

        public void Activate()
        {
 
            if(this.IsActive == true)
            {
                throw new ArgumentException("User alerady active");
            }

            this.IsActive = true;
        }

        public void Deactivate()
        {

            if (this.IsActive == false)
            {
                throw new ArgumentException("User alerady inactive");
            }

            this.IsActive = false;
        }

    }
}
