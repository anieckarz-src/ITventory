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
    public class Room: Entity
    {
        public Guid Id { get; init; }
        public int OfficeId { get; private set; }
        public int Floor { get; private set; }
        public float? Area { get; private set; }
        public int Capacity { get;private set; }
        public int PersonResponsibleId {  get; private set; }

        private List<InventoryProduct> _roomInventory = new List<InventoryProduct>();
        //Lista osob
        public ReadOnlyCollection<InventoryProduct> RoomInventory => _roomInventory.AsReadOnly();


        private Room()
        {

        }

        public Room(int officeId, int floor, float area, int capacity, int personResponsibleId)
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


            Id = Guid.NewGuid();
            OfficeId = officeId;
            Floor = floor;
            Area = area;
            Capacity = capacity;
            PersonResponsibleId = personResponsibleId;


        }

        public void AddInventory(Product product, int sku)
        {
            var inventory = _roomInventory.FirstOrDefault(i => i.Product == product);

            if (inventory == null)
            {
                var newInventory = InventoryProduct.Create(this.Id, product, sku);
                _roomInventory.Add(newInventory);
            }
            else
            {
                inventory?.AddSku(sku);
            }
            
        }

        public void ReduceInventory(Product product, int sku)
        {
            var inventory = _roomInventory.FirstOrDefault(i => i.Product == product);

            if(inventory == null)
            {
                throw new ArgumentException("Cannot reduce inventory: inventory doesn't exist");
            }

            inventory?.ReduceSku(sku);
        }

    }
}
