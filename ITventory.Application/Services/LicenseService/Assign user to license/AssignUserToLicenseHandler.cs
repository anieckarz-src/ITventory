using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Assign_user_to_license
{
    public sealed class AssignUserToLicenseHandler : ICommandHandler<AssignUserToLicense>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILicenseRepository _licenseRepository;

        public AssignUserToLicenseHandler(IEmployeeRepository employeeRepository, ILicenseRepository licenseRepository)
        {
            _employeeRepository = employeeRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task HandleAsync(AssignUserToLicense command)
        {
            var (licenseId, userId) = command;

            var license = await _licenseRepository.GetAsync(licenseId);
            var user = await _employeeRepository.GetAsync(userId);

            if(license == null)
            {
                throw new ArgumentException("License not found");
            }
            if(user == null)
            {
                throw new ArgumentException("User not found");
            }

            license.AssignToUser(user);
            await _employeeRepository.UpdateAsync(user);
            //Przypisanie po username
        }
    }
}
