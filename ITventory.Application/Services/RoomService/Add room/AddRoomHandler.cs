using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.Add_room
{
    public sealed class AddRoomHandler : ICommandHandler<AddRoom>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IOfficeRepository _officeRepository;

        public AddRoomHandler(IEmployeeRepository employeeRepository, IRoomRepository roomRepository, IOfficeRepository officeRepository)
        {
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
            _officeRepository = officeRepository;
        }

        public async Task HandleAsync(AddRoom command)
        {
            var (officeId, floor, area, capacity, personResponsibleId, name) = command;

            if (!await _officeRepository.ExistsById(officeId))
            {
                throw new InvalidOperationException("Office not found");
            }
            if(!await _employeeRepository.ExistsById(personResponsibleId))
            {
                throw new InvalidOperationException("Person not found");
            }

            if(await _roomRepository.RoomExistsInOffice(officeId, name))
            {
                throw new InvalidOperationException("Room with this name already exist");
            }

            var room = Room.Create(officeId, name, floor, area, capacity, personResponsibleId);

            await _roomRepository.AddAsync(room);
        }
    }
}
