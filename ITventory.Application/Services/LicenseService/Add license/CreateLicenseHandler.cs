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
        public async Task HandleAsync(CreateLicense command)
        {
           var (licenseType, licenseKey, validUntil, maxUse) = command;

           SoftwareLicense license = SoftwareLicense.Create(licenseType, licenseKey, validUntil, maxUse);

            _licenseRepository.AddAsync(license);
        }
    }
}
