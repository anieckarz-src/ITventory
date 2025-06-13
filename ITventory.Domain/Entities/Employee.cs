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
        public string IdentityId { get; private set; }
        public Username Username { get; init; }
        public string? Name { get; private set; }
        public string? LastName { get; private set; }
        public bool IsActive { get; private set; }
        public Area? Area { get; private set; }
        public string? PositionName { get; private set; }
        public Seniority? Seniority { get; private set; }
        public Guid? ManagerId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public DateOnly? HireDate { get; private set; }
        public DateOnly? BirthDate { get; private set; }
        public Guid? RoomId { get; private set; }
        public int Experience => HireDate.HasValue
        ? DateOnly.FromDateTime(DateTime.UtcNow).Year - HireDate.Value.Year
        : 0;



        private Employee()
        {

        }

        private Employee(Username username, string identityId)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Invalid identity id reference");
            }
            Id = Guid.NewGuid();
            IdentityId = identityId;
            Username = username;
            IsActive = true;
        }

        public static Employee CreateMinimal(Username username, string identityId)
        {
            return new Employee(username, identityId);
        }

        public void SetDetails(string name, string lastName, Area area, string positionName,
                                  Seniority seniority, Guid managerId, Guid departmentId, DateOnly hireDate,
                                  DateOnly birthDate, Guid roomId)
        {
            Name = name;
            LastName = lastName;
            Area = area;
            PositionName = positionName;
            Seniority = seniority;
            ManagerId = managerId;
            DepartmentId = departmentId;
            HireDate = hireDate;
            BirthDate = birthDate;
            RoomId = roomId;
            
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
