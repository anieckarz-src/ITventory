using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Entities;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_rating
{
    internal sealed class AddSoftwareRatingHandler : ICommandHandler<AddSoftwareRating>
    {
        private readonly ISoftwareRepository _softwareRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddSoftwareRatingHandler(ISoftwareRepository softwareRepository, IEmployeeRepository employeeRepository)
        {
            _softwareRepository = softwareRepository;
            _employeeRepository = employeeRepository;
        }



        public async Task HandleAsync(AddSoftwareRating command)
        {
            var (reviwedSoftwareVersionId, reviewedSoftwareId, ratingMark, ratedById) = command;

            
            var user = await _employeeRepository.GetAsync(ratedById) ?? throw new InvalidOperationException("User not found");

            var software = await _softwareRepository.GetAsync(reviewedSoftwareId)
              ?? throw new InvalidOperationException("Software not found");

            var version = software.SoftwareVersions.FirstOrDefault(v => v.Id == reviwedSoftwareVersionId)
                          ?? throw new InvalidOperationException("Software version not found");

            var rating = RatingSoftwareVersion.Create(reviewedSoftwareId, ratingMark, ratedById);


            version.AddRating(rating);
            await _softwareRepository.UpdateAsync(software);


            version.AddRating(rating);

            await _softwareRepository.UpdateAsync(software);

        }
    }
}
