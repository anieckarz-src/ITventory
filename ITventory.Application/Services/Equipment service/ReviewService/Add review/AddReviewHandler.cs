using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.ReviewService.Add_review
{
    internal sealed class AddReviewHandler : ICommandHandler<AddReview>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public AddReviewHandler(IEmployeeRepository employeeRepository, IEquipmentRepository equipmentRepository)
        {
            _employeeRepository = employeeRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task HandleAsync(AddReview command)
        {
            var (equipmentId, reviewerId, details, reviewDate, condition) = command;

            var user = await _employeeRepository.GetAsync(reviewerId) ?? throw new InvalidOperationException("User not found");
            var equipment = await _equipmentRepository.GetAsync(equipmentId) ?? throw new InvalidOperationException("Equipment not found");
            var newReview = Review.Create(equipmentId, reviewerId, details, reviewDate, condition);

            equipment.AddReview(newReview);
            // Repozytoria tylko dla aggregate rootów - usuwam repozytorium dla review

            await _equipmentRepository.UpdateAsync(equipment);
            


        }
    }
}
