using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.SoftwareVersionService.Set_default_version
{
    internal sealed class SetDefaultVersionHandler : ICommandHandler<SetDefaultVersion>
    {
        private readonly ISoftwareRepository _softwareRepository;

        public SetDefaultVersionHandler(ISoftwareRepository softwareRepository)
        {
            _softwareRepository = softwareRepository;
        }

        public async Task HandleAsync(SetDefaultVersion command)
        {
            var (softwareId, versionId) = command;

            var software = await _softwareRepository.GetAsync(softwareId) ?? throw new InvalidOperationException("Software not found");
            var version = await _softwareRepository.GetSoftwareVersionById(softwareId) ?? throw new InvalidOperationException("Software version not found");

            software.SetDefaultVersion(versionId);
            await _softwareRepository.UpdateAsync(software);
        }
    }
}
