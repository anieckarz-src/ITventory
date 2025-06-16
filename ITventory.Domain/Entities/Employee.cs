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
            // Validate string inputs
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName cannot be null or empty.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(positionName))
                throw new ArgumentException("PositionName cannot be null or empty.", nameof(positionName));

            // Validate enums
            if (!Enum.IsDefined(typeof(Area), area))
                throw new ArgumentException("Invalid Area value.", nameof(area));

            if (!Enum.IsDefined(typeof(Seniority), seniority))
                throw new ArgumentException("Invalid Seniority value.", nameof(seniority));

            // Validate GUIDs
            if (managerId == Guid.Empty)
                throw new ArgumentException("ManagerId cannot be empty.", nameof(managerId));

            if (departmentId == Guid.Empty)
                throw new ArgumentException("DepartmentId cannot be empty.", nameof(departmentId));

            if (roomId == Guid.Empty)
                throw new ArgumentException("RoomId cannot be empty.", nameof(roomId));

            // Validate Dates
            if (birthDate > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("BirthDate cannot be in the future.", nameof(birthDate));

            if (hireDate > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("HireDate cannot be in the future.", nameof(hireDate));

            if (birthDate > hireDate)
                throw new ArgumentException("BirthDate cannot be after HireDate.");

            // Set properties after validation
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
