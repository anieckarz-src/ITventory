namespace ITventory.Domain.AbstractClasses
{
    public abstract class Item
    {
        public Guid Id { get; init; }
        public string? Description { get; private set; }
        public double Worth { get; private set; }
        public Guid ProducentId { get; init; }
        public Guid ModelId { get; init; }
        public int ModelYear { get; init; }
        public string SerialNumber { get; init; }
        public DateOnly? PurchasedDate { get; private set; }
        public Guid RoomId {  get; private set;}
        public Guid DepartmentId { get; private set; }

        protected Item()
        {

        }

        public Item(string description, double worth, Guid producentId, Guid modelId, int modelYear, string serialNumber, DateOnly purchasedDate,
            Guid roomId, Guid departmentId)
        {

            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentNullException(nameof(serialNumber));
            }
            if(modelYear < 1999 || modelYear > DateTime.Now.Year)
            {
                throw new ArgumentException("Invalid model year");
            }
            if(purchasedDate.Year < 1999 || purchasedDate.Year > DateTime.Now.Year)
            {
                throw new ArgumentException($"Purchase date must be between 1999 and {DateTime.Now.Year}");
            }

            if(worth <= 0)
            {
                throw new ArgumentException("Worth cannot be negative");
            }

            this.Description = description;
            this.Worth = worth;
            this.ProducentId = producentId;
            this.ModelId = modelId;
            this.ModelYear = modelYear;
            this.SerialNumber = serialNumber;
            this.PurchasedDate = purchasedDate;
            this.RoomId = roomId;
            this.DepartmentId = departmentId;
        }

        public void ChangeRoom(Room room)
        {
            if(room == null)
            {
                throw new ArgumentNullException("Room is empty here");
            }

            if(room.Id == this.RoomId)
            {
                throw new ArgumentException("Cannot move equipment to the same room");
            }

        }


    }
}
