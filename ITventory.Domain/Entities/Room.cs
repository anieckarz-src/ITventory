using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Room
    {
        public Guid Id { get; init; }
        public Guid OfficeId { get; private set; }
        public string RoomName { get; private set; }
        public int Floor { get; private set; }
        public double? Area { get; private set; }
        public int Capacity { get;private set; }
        public Guid PersonResponsibleId {  get; private set; }

        public List<Employee> Employees { get; private set; } = new();
        public List<InventoryProduct> RoomInventory { get; private set; } = new();


        private Room()
        {

        }

        public void AssignToRoom(Employee employee)
        {
            if(employee == null)
            {
                throw new ArgumentNullException("Invalid employee id");
            }

            if(Employees.Count >= Capacity)
            {
                throw new InvalidOperationException("Room capacity limit has been met");
            }
            Employees.Add(employee);
        }

        public void RemoveFromRoom(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("Invalid employee id");
            }

            if(! Employees.Any(e => e.Id == employee.Id)){
                throw new InvalidOperationException("User not in the room");
            }

            Employees.Remove(employee);
                
        }

        public Room(Guid officeId, string roomName, int floor, double area, int capacity, Guid personResponsibleId)
        {
            if (floor < -1 || floor > 10)
            {
                throw new ArgumentException("Invalid floor number - it must vary between -1 and 10");
            }
            if (area < 5 || area > 2000)
            {
                throw new ArgumentException("Area must be between 5 and 2000");
            }
            if (capacity < 2 || capacity > 100)
            {
                throw new ArgumentException("Capacity must be between 2 and 100");
            }
            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new ArgumentException("Room name cannot be empty");
            }


            Id = Guid.NewGuid();
            OfficeId = officeId;
            Floor = floor;
            Area = area;
            Capacity = capacity;
            PersonResponsibleId = personResponsibleId;
            RoomName = roomName;


        }

        public static Room Create(Guid officeId, string roomName, int floor, double area, int capacity, Guid personResponsibleId)
        {
            return new Room(officeId, roomName, floor, area, capacity, personResponsibleId);
        }


        public void AddInventory(Product product, int sku)
        {
            var inventory = RoomInventory.FirstOrDefault(i => i.Product == product);

            if (inventory == null)
            {
                var newInventory = InventoryProduct.Create(this.Id, product, sku);
                RoomInventory.Add(newInventory);
            }

            else
            {
                inventory?.AddSku(sku);
            }
            
        }

        public void ReduceInventory(Product product, int sku)
        {
            var inventory = RoomInventory.FirstOrDefault(i => i.Product == product);

            if(inventory == null)
            {
                throw new ArgumentException("Cannot reduce inventory: inventory doesn't exist");
            }
            inventory?.ReduceSku(sku);
        }

    }
}
