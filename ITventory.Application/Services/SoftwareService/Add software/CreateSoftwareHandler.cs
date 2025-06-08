using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.Add_software
{
    internal sealed class CreateSoftwareHandler : ICommandHandler<CreateSoftware>
    {
        private readonly ISoftwareRepository _softwareRepository;
        private readonly IProducentRepository _producentRepository;

        public CreateSoftwareHandler(ISoftwareRepository softwareRepository, IProducentRepository producentRepository)
        {
            _softwareRepository = softwareRepository;
            _producentRepository = producentRepository;
        }

        public async Task HandleAsync(CreateSoftware command)
        {
            var(publisherId, approvalType, name, softwareVersionId) = command;

            if (await _producentRepository.ExistsById(publisherId) == false)
            {
                throw new InvalidOperationException("Publisher not found");
            }

            var software = Software.Create(publisherId, approvalType, name);
            await _softwareRepository.UpdateAsync(software);


        }
    }
}
