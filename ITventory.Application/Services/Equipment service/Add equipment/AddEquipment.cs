using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.Equipment_service.Add_equipment
{
    public record AddEquipment(string description, double worth, Guid producentId, Guid modelId, int modelYear, string serialNumber,
                         DateOnly purchasedDate, Guid roomId, Guid departmentId, Condition condition): ICommand_;
  
}
