using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.Equipment_service.Add_equipment
{
    public sealed class AddEquipmentHandler : ICommandHandler<AddEquipment>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IProducentRepository _producentRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public AddEquipmentHandler(IModelRepository modelRepository, IProducentRepository producentRepository, IRoomRepository roomRepository, IDepartmentRepository departmentRepository, IEquipmentRepository equipmentRepository)
        {
            _modelRepository = modelRepository;
            _producentRepository = producentRepository;
            _roomRepository = roomRepository;
            _departmentRepository = departmentRepository;
            _equipmentRepository = equipmentRepository;

        }



        public async Task HandleAsync(AddEquipment command)
        {
            var (description, worth, producentId, modelId, modelYear, serialNumber,
                          purchasedDate, roomId, departmentId, condition) = command;

            var model = await _modelRepository.GetAsync(modelId) ?? throw new InvalidOperationException("Model not found");

            if(await _producentRepository.ExistsById(producentId) == false){
                throw new InvalidOperationException("Producent not found");
            }
            if(await _roomRepository.ExistsById(roomId) == false)
            {
                throw new InvalidOperationException("Room not found");
            }
            if(await _departmentRepository.ExistsById(departmentId) == false)
            {
                throw new InvalidOperationException("Department not found");
            }

            var equipment = Equipment.Create(description, worth, producentId, modelId, modelYear, serialNumber, purchasedDate, roomId, departmentId, condition);

            await _equipmentRepository.AddAsync(equipment);
        }
    }
}
