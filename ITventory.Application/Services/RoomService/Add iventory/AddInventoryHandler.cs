using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.Add_iventory
{
    internal sealed class AddInventoryHandler : ICommandHandler<AddInventory>
    {
        private readonly IProductRepository _productRepository;
        private readonly IRoomRepository _roomRepository;

        public AddInventoryHandler(IProductRepository productRepository, IRoomRepository roomRepository)
        {
            _productRepository = productRepository;
            _roomRepository = roomRepository;
        }


        public async Task HandleAsync(AddInventory command)
        {
            var (roomId, productId, sku) = command;
            var room = await _roomRepository.GetAsync(roomId) ?? throw new InvalidOperationException("Room not found");

            var product = await _productRepository.GetAsync(productId) ?? throw new InvalidOperationException("Product not found");

            room.AddInventory(product, sku);
            await _roomRepository.UpdateAsync(room);
        }
    }
}
