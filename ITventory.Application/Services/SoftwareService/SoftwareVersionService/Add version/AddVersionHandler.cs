using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_version
{
    internal sealed class AddVersionHandler : ICommandHandler<AddVersion>
    {
        private readonly ISoftwareRepository _softwareRepository;

        public AddVersionHandler(ISoftwareRepository softwareRepository)
        {
            _softwareRepository = softwareRepository;
        }

        public async Task HandleAsync(AddVersion command)
        {
            var (softwareId, versionNumber, price, published, licenseType) = command;

            var software = await _softwareRepository.GetAsync(softwareId) ?? throw new InvalidOperationException("Software not found");

            var version = SoftwareVersion.Create(softwareId, versionNumber, price, published, licenseType);
            software.AddVersion(version);
            await _softwareRepository.UpdateAsync(software);

            
        }
    }
}
