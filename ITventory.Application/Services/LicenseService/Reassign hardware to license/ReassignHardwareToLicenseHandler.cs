using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Reassign_hardware_to_license
{
    internal sealed class ReassignHardwareToLicenseHandler : ICommandHandler<ReassignHardwareToLicense>
    {
        private readonly IHardwareRepository _hardwareRepository;
        private readonly ILicenseRepository _licenseRepository;

        public ReassignHardwareToLicenseHandler(IHardwareRepository hardwareRepository, ILicenseRepository licenseRepository)
        {
            _hardwareRepository = hardwareRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task HandleAsync(ReassignHardwareToLicense command)
        {
            var (licenseId, newHardwareId, oldHardwareId) = command;

            var license = await _licenseRepository.GetAsync(licenseId)
                ?? throw new InvalidOperationException("License not found");

            var newHardware = await _hardwareRepository.GetAsync(newHardwareId)
                ?? throw new InvalidOperationException("New hardware not found");

            var oldHardware = await _hardwareRepository.GetAsync(oldHardwareId)
                ?? throw new InvalidOperationException("Old hardware not found");

            license.ReassignLicenseToHardware(newHardware, oldHardware);
            await _licenseRepository.UpdateAsync(license);
        }
    }
}
