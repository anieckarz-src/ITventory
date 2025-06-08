using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Domain;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Add_license
{
    internal sealed class CreateLicenseHandler : ICommandHandler<CreateLicense>
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ISoftwareRepository _softwareRepository;

        public CreateLicenseHandler(ILicenseRepository licenseRepository, ISoftwareRepository softwareRepository)
        {
            _licenseRepository = licenseRepository;
            _softwareRepository = softwareRepository;
        }

        public async Task HandleAsync(CreateLicense command)
        {
           var (licenseType, licenseKey, validUntil, maxUse, softwareVersionId, softwareId) = command;


            var sofware = await _softwareRepository.GetAsync(softwareId) ?? throw new InvalidOperationException("Software not found");
            if(!sofware.SoftwareVersions.Any(s => s.Id == softwareVersionId))
            {
                throw new InvalidOperationException("Software version not found");
            }

           SoftwareLicense license = SoftwareLicense.Create(licenseType, licenseKey, validUntil, maxUse, softwareVersionId);

            await _licenseRepository.AddAsync(license);
        }
    }
}
