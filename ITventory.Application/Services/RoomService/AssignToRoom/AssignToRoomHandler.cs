using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.AssignToRoom
{
    public sealed class AssignToRoomHandler : ICommandHandler<AssignToRoom>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AssignToRoomHandler(IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task HandleAsync(AssignToRoom command)
        {
            var (roomId, employeeId) = command;

            var room = await _roomRepository.GetAsync(roomId) ?? throw new InvalidOperationException("Room does not exist");
            var user = await _employeeRepository.GetAsync(employeeId.Id) ?? throw new InvalidOperationException("User does not exist");

            room.AssignToRoom(employeeId);
            await _roomRepository.UpdateAsync(room);
        }
    }
}
