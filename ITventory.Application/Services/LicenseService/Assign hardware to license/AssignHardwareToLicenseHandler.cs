using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Assign_hardware_to_license
{
    public sealed class AssignHardwareToLicenseHandler : ICommandHandler<AssignHardwareToLicense>
    {
        private readonly IHardwareRepository _hardwareRepository;
        private readonly ILicenseRepository _licenseRepository;

        public AssignHardwareToLicenseHandler(IHardwareRepository hardwareRepository, ILicenseRepository licenseRepository)
        {
            _hardwareRepository = hardwareRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task HandleAsync(AssignHardwareToLicense command)
        {
            var (licenseId, hardwareId) = command;

            var license = await _licenseRepository.GetAsync(licenseId);

            if (license == null)
            {
                throw new InvalidOperationException("License not found");
            }

            var hardware = await _hardwareRepository.GetAsync(hardwareId);

            if (hardware == null)
            {
                throw new InvalidOperationException("Hardware not found");
            }

            license.AssignToHardware(hardware);
            await _licenseRepository.UpdateAsync(license);
        }
    }
}
