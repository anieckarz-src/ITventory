using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Reassign_user_to_license
{
    public sealed class ReassignUserToLicenseHandler : ICommandHandler<ReassignUserToLicense>
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ReassignUserToLicenseHandler(ILicenseRepository licenseRepository, IEmployeeRepository employeeRepository)
        {
            _licenseRepository = licenseRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task HandleAsync(ReassignUserToLicense command)
        {
            var (licenseId, newUserId, oldUserId) = command;

            var license = await _licenseRepository.GetAsync(licenseId)
            ?? throw new InvalidOperationException("License not found");

            var newUser = await _employeeRepository.GetAsync(newUserId)
            ?? throw new InvalidOperationException("New user not found");

            var oldUser = await _employeeRepository.GetAsync(oldUserId)
            ?? throw new InvalidOperationException("Old user not found");

            license.ReassignLicenseToUser(newUser, oldUser);
            await _licenseRepository.UpdateAsync(license);
        }
    }
}
