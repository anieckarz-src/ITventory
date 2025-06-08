using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface ILicenseRepository
    {
        Task<SoftwareLicense> GetAsync(Guid licenseId);
        Task AddAsync(SoftwareLicense license);
        Task UpdateAsync(SoftwareLicense license);
        Task DeleteAsync(SoftwareLicense license);

        //read service w repozytorium
        Task<bool> ExistsById(Guid licenseId);

    }
}
    

