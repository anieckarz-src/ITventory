using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.Services.RoomService.Add_iventory;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.Reduce_inventory
{
    public sealed class ReduceInventoryHandler : ICommandHandler<ReduceInventory>
    {

        private readonly IProductRepository _productRepository;
        private readonly IRoomRepository _roomRepository;

        public ReduceInventoryHandler(IProductRepository productRepository, IRoomRepository roomRepository)
        {
            _productRepository = productRepository;
            _roomRepository = roomRepository;
        }


        public async Task HandleAsync(ReduceInventory command)
        {
            var (roomId, productId, sku) = command;
            var room = await _roomRepository.GetAsync(roomId) ?? throw new InvalidOperationException("Room not found");

            var product = await _productRepository.GetAsync(productId) ?? throw new InvalidOperationException("Product not found");

            room.ReduceInventory(product, sku);
            await _roomRepository.UpdateAsync(room);
        }
    }
}


